using Spiderman;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace Spandex
{
    public partial class Form1 : Form
    {
        public Material[] materials;
        public Dictionary<uint, GridEntry> textures { get; set; }
        public Dictionary<uint, GridEntry[]> values { get; set; }
        string lastsourcedir, lastoutputdir, lastsavefile;

        public Form1(string[] argv)
        {
            InitializeComponent();
            materials = new Material[2];

            textures = new Dictionary<uint, GridEntry>();
            stringGrid.DataSource = textures.Values.ToList();
            stringGrid.AutoResizeColumns();

            values = new Dictionary<uint, GridEntry[]>();
            valueGrid.DataSource = textures.Values.ToList();
            valueGrid.CellValueChanged += ValueGrid_CellValueChanged;

            if (argv.Length > 0 && File.Exists(argv[0]))
                Open(argv[0]);

            statusLabel.Image = null;
            statusLabel.Text = "Open an material file";
        }

        private void openbutton_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void Open(string filename = "")
        {
            this.Text = $"Spandex";
            textures = new Dictionary<uint, GridEntry>();
            values = new Dictionary<uint, GridEntry[]>();
            statusLabel.Image = null;
            statusLabel.Text = "Open an material file";
            savebutton.Enabled = false;

            var f = new OpenFileDialog();
            f.Filter = "Material asset|*.material;*.materialgraph";
            if (lastsourcedir is not null)
                f.InitialDirectory = lastsourcedir;
            f.FileName = filename;

            if (f.FileName != "" || f.ShowDialog() == DialogResult.OK)
            {
                UseWaitCursor = true;
                Application.DoEvents();

                lastsourcedir = Path.GetDirectoryName(f.FileName) + @"\";
                lastsavefile = Path.ChangeExtension(Path.GetFileName(f.FileName), $".modified{Path.GetExtension(f.FileName)}");

                ProcessFile(f.FileName, 0);

                savebutton.Enabled = true;

                if (textures.ContainsKey(0) && textures[0].Value != null && textures[0].Value != "")
                {
                    string trytemplate = lastsourcedir + (textures[0].Value as string).Replace('\\', '_').Replace('/', '_');
                    if (File.Exists(trytemplate))
                    {
                        ProcessFile(trytemplate, 1);
                        statusLabel.Image = global::Spandex.Properties.Resources.ok;
                        statusLabel.Text = $"Template loaded: {trytemplate}";
                    }
                    else
                    {
                        statusLabel.Image = global::Spandex.Properties.Resources.warning;
                        statusLabel.Text = $"Template could not be found: {trytemplate}";
                    }
                }
                else
                {
                    statusLabel.Image = global::Spandex.Properties.Resources.ok;
                    statusLabel.Text = $"Material loaded";
                }

                UseWaitCursor = false;
                this.Text = $"{materials[0].assetfile} - Spandex";
            }

            stringGrid.DataSource = textures.Values.
                OrderBy(v => (v as GridEntry).ID).
                ToList();
            stringGrid.AutoResizeColumns();

            valueGrid.DataSource = values.Values.
                SelectMany(v => v).
                OrderBy(v => v.Type == GridEntry.TypeOrder.Integer ? 1 : 0 ).
                ThenBy(v => v.ID).
                ToList();
            valueGrid.AutoResizeColumns();
        }

        private void ProcessFile(string filename, int entry)
        {
            materials[entry] = new Material(filename);
            Material material = materials[entry];
            var slotoverride = entry > 0 ? GridEntry.SLOTEXTERNAL : GridEntry.SLOTOVERRIDE;
            var slotshader = entry > 0 ? GridEntry.SLOTEXTERNAL : GridEntry.SLOTINTERNAL;

            var tl = material.GetSection<Material.ShaderTextures>();
            if (tl == null)
            {
                if (material.segments.Length > 0)
                {
                    (var segstrings, var segoffsets) = Material.UnpackStrings(material.segments[0].data);
                    if (segstrings.Count == 1)
                    {
                        textures.TryAdd(0, new GridEntry { ID = 0 });
                        textures[0].values[slotoverride].Value = segstrings[0];
                    }
                }
            }
            else if (tl.entries.Count > 0)
            {
                var dataseg = material.sectionlayout.GetSectionByOffset(((Material.ShaderTextures.ShaderTextureEntry)tl.entries[0]).nameoffset);
                if (dataseg != null)
                {
                    (var segstrings, var segoffsets) = Material.UnpackStrings(dataseg.data);
                    if (segoffsets.Where(s => s != 0).SequenceEqual(tl.entries.Values.Cast<Material.ShaderTextures.ShaderTextureEntry>().Select(s => s.nameoffset - dataseg.offset).Where(s => s != 0)))
                    {
                        if (dataseg is Material.Headerless)
                            ((Material.Headerless)dataseg).moveable = true;

                        int currentstr = 0;
                        if (((Material.ShaderTextures.ShaderTextureEntry)tl.entries[0]).nameoffset != dataseg.offset)
                        {
                            textures.TryAdd(0, new GridEntry { ID = 0 });
                            textures[0].values[slotoverride].Value = segstrings[currentstr++];
                        }

                        for (int i = 0; i < tl.entries.Count; i++)
                        {
                            var v = (Material.ShaderTextures.ShaderTextureEntry)tl.entries[i];
                            if (v.nameoffset != segoffsets[currentstr] + dataseg.offset)
                                throw new Exception("Parsing assumption failure");
                            textures.TryAdd(v.InputID, new GridEntry { ID = v.InputID });
                            textures[v.InputID].values[slotshader].Slot = (uint)i;
                            textures[v.InputID].values[slotshader].Value = segstrings[currentstr++];
                        }
                    }
                }
            }

            var material8 = material.GetSection<Material.ShaderOverrides>();
            if (material8 != null)
            {
                for (uint i = 0; i < material8.Textures.Count; i++)
                {
                    var s = material8.Textures.Cast<DictionaryEntry>().ElementAt((int)i);
                    textures.TryAdd((uint)s.Key, new GridEntry { ID = (uint)s.Key });
                    textures[(uint)s.Key].values[slotoverride].Slot = i;
                    textures[(uint)s.Key].values[slotoverride].Value = (string)s.Value;
                }

                for (uint i = 0; i < material8.Floats.Count; i++)
                {
                    var s = material8.Floats.Cast<DictionaryEntry>().ElementAt((int)i);
                    List<object> vs = null;

                    GridEntry.TypeOrder? type = null;
                    switch (s.Value)
                    {
                        case float[]:
                            vs = ((float[])s.Value).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Float;
                            break;
                        case ushort[]:
                            vs = ((ushort[])s.Value).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Short;
                            break;
                        case byte[]:
                            vs = ((byte[])s.Value).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Byte;
                            break;

                    }

                    values.TryAdd((uint)s.Key, new GridEntry[vs.Count]);
                    var varray = values[(uint)s.Key];
                    for (int j = 0; j < vs.Count; j++)
                    {
                        if (varray[j] == null)
                        {
                            varray[j] = new GridEntry();
                            varray[j].Span = vs.Count > 1 ?  j / (float)(vs.Count - 1) : null;
                            varray[j].ID = (uint)s.Key;
                        }

                        varray[j].values[slotoverride].Slot = i;
                        varray[j].Type = type;
                        varray[j].values[slotoverride].Value = vs[j];
                    }
                }

                for (uint i = 0; i < material8.Ints.Count; i++)
                {
                    var n = material8.Ints.Cast<DictionaryEntry>().ElementAt((int)i);
                    values.TryAdd((uint)n.Key, new GridEntry[1]);
                    var varray = values[(uint)n.Key];
                    varray[0] = new GridEntry();
                    varray[0].ID = (uint)n.Key;
                    varray[0].Type = GridEntry.TypeOrder.Integer;
                    varray[0].values[slotoverride].Slot = i;
                    varray[0].values[slotoverride].Value = n.Value;
                }
            }

            var shaderfloats = material.GetSection<Material.ShaderFloats>();
            if (shaderfloats != null)
            {
                for (uint i = 0; i < shaderfloats.definitions.Length; i++)
                {
                    var c = shaderfloats.definitions[i];
                    var v = shaderfloats.values[(int)i];
                    List<object> vs = new List<object>();

                    GridEntry.TypeOrder? type = null;
                    switch (v)
                    {
                        case float[]:
                            vs = ((float[])v).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Float;
                            break;
                        case ushort[]:
                            vs = ((ushort[])v).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Short;
                            break;
                        case byte[]:
                            vs = ((byte[])v).Cast<object>().ToList();
                            type = GridEntry.TypeOrder.Byte;
                            break;

                    }

                    values.TryAdd((uint)c.ID, new GridEntry[vs.Count]);
                    var varray = values[(uint)c.ID];
                    for (int j = 0; j < vs.Count; j++)
                    {
                        if (varray[j] == null)
                        {
                            varray[j] = new GridEntry();
                            varray[j].Span = vs.Count > 1 ? j / (float)(vs.Count - 1) : null;
                            varray[j].ID = (uint)c.ID;
                        }

                        varray[j].values[slotshader].Slot = i;
                        varray[j].Type = type;
                        varray[j].values[slotshader].Value = vs[j];
                    }
                }
            }

            var shaderints = material.GetSection<Material.ShaderIntegers>();
            if (shaderints != null)
            {
                for (int i = 0; i < shaderints.integers.Count; i++)
                {
                    uint v = ((uint)shaderints.integers[i]);
                    var id = shaderints.integers.Keys.Cast<uint>().ElementAt(i);

                    values.TryAdd(id, new GridEntry[1]);
                    values[id][0] ??= new GridEntry();
                    values[id][0].ID = id;
                    values[id][0].Type = GridEntry.TypeOrder.Integer;
                    values[id][0].values[slotshader].Slot = (uint)i;
                    values[id][0].values[slotshader].Value = v;
                }
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            var material = materials[0];

            var f = new SaveFileDialog();
            switch (material.ps4header.type)
            {
                case Material.SectionType.MATERIALTEMPLATE:
                    f.Filter = "Material template asset|*.materialgraph";
                    break;
                default:
                    f.Filter = "Material asset|*.material";
                    break;
            }
            if (lastoutputdir is not null)
                f.InitialDirectory = Path.GetDirectoryName(lastoutputdir);
            f.FileName = lastsavefile;

            if (f.ShowDialog() == DialogResult.OK)
            {
                lastoutputdir = Path.GetDirectoryName(f.FileName) + @"\";
                lastsavefile = f.FileName;
                UseWaitCursor = true;
                Application.DoEvents();

                if (material.assetfile == f.FileName)
                {
                    MessageBox.Show("Can't overwrite the original. Choose a new filename.", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                // reset
                materials[0] = new Material(material.assetfile);
                material = materials[0];


                var tl = material.GetSection<Material.ShaderTextures>();
                var datastrings = new List<string>();
                var material8 = material.GetSection<Material.ShaderOverrides>();
                var m8idx = 0;
                Material.SectionHeader dataseg = tl == null ? material.segments[0] : material.sectionlayout.GetSectionByOffset(((Material.ShaderTextures.ShaderTextureEntry)tl.entries[0]).nameoffset);

                if (textures[0].ID == 0)
                {
                    datastrings.Add((string?)textures[0].values[GridEntry.SLOTOVERRIDE].Value ?? String.Empty);
                }

                foreach (var kv in textures)
                {
                    if (kv.Key == 0)
                        continue;

                    if (tl != null && kv.Value.values[GridEntry.SLOTINTERNAL].Slot != null)
                        datastrings.Add(((string?)kv.Value.values[GridEntry.SLOTINTERNAL].Value) ?? String.Empty);

                    if (!removeUndefTextures.Checked || materials[1] == null || kv.Value.values[GridEntry.SLOTEXTERNAL].Slot != null)
                        material8.Textures[kv.Key] = kv.Value.values[GridEntry.SLOTOVERRIDE].Value ?? String.Empty;
                    else if (material8.Textures.Contains(kv.Key))
                        material8.Textures.Remove(kv.Key);
                }

                (dataseg.newdata, var offsets) = Material.PackStrings(datastrings);

                if (tl != null)
                {
                    offsets = offsets.Skip(offsets.Length - tl.entries.Count).ToArray();
                    for (int i = 0; i < tl.entries.Count; i++)
                        ((Material.ShaderTextures.ShaderTextureEntry)tl.entries[i]).nameoffset = offsets[i] + dataseg.offset;
                }


                var shaderfloats = material.GetSection<Material.ShaderFloats>();
                var shaderints = material.GetSection<Material.ShaderIntegers>();
                foreach (var kv in values)
                {
                    var k = kv.Key;
                    var v = kv.Value;

                    if (v[0].values[GridEntry.SLOTINTERNAL].Slot != null)
                    {
                        // internal template
                        object[] varray = v.Select(v => v.values[GridEntry.SLOTINTERNAL].Value).ToArray();
                        int slot = (int)v[0].values[GridEntry.SLOTINTERNAL].Slot;

                        switch (v[0].Type)
                        {
                            case GridEntry.TypeOrder.Float:
                                switch (varray[0])
                                {
                                    case float:
                                        shaderfloats.values[slot] = varray.Select(f => f ?? 0f).Cast<float>().ToArray();
                                        break;
                                    case ushort:
                                        shaderfloats.values[slot] = varray.Select(f => f ?? 0f).Cast<ushort>().ToArray();
                                        break;
                                    case byte:
                                        shaderfloats.values[slot] = varray.Select(f => f ?? 0f).Cast<byte>().ToArray();
                                        break;
                                }
                                break;
                            case GridEntry.TypeOrder.Integer:
                                shaderints.integers[slot] = (uint)(varray[0] ?? v[0].values[GridEntry.SLOTEXTERNAL].Value);
                                break;
                        }
                    }

                    if (v[0].values[GridEntry.SLOTOVERRIDE].Slot != null || v[0].values[GridEntry.SLOTEXTERNAL].Slot != null)
                    {
                        // material8
                        object[] varray = v.Select(v => v.values[GridEntry.SLOTOVERRIDE].Value).ToArray();

                        switch (v[0].Type)
                        {
                            case GridEntry.TypeOrder.Float:
                                if (!varray.All(v => v == null) && (!removeUndefFloats.Checked || materials[1] == null || v[0].values[GridEntry.SLOTEXTERNAL].Slot != null))
                                    switch (varray[0])
                                    {
                                        case float:
                                            material8.Floats[k] = varray.Select((f, i) => f = f ?? v[i].values[GridEntry.SLOTEXTERNAL].Value).Cast<float>().ToArray();
                                            break;
                                        case ushort:
                                            material8.Floats[k] = varray.Select((f, i) => f = f ?? v[i].values[GridEntry.SLOTEXTERNAL].Value).Cast<ushort>().ToArray();
                                            break;
                                        case byte:
                                            material8.Floats[k] = varray.Select((f, i) => f = f ?? v[i].values[GridEntry.SLOTEXTERNAL].Value).Cast<byte>().ToArray();
                                            break;
                                    }
                                else if (material8.Floats.Contains(k))
                                    material8.Floats.Remove(k);
                                break;

                            case GridEntry.TypeOrder.Integer:
                                if (!varray.All(v => v == null) && (!removeUndefInts.Checked || materials[1] == null || v[0].values[GridEntry.SLOTEXTERNAL].Slot != null))
                                    material8.Ints[k] = (uint)(varray[0] ?? v[0].values[GridEntry.SLOTEXTERNAL].Value);
                                else if (material8.Ints.Contains(k))
                                    material8.Ints.Remove(k);
                                break;
                        }
                    }
                }

                if (shaderfloats != null)
                    // hack, but why is this in a separate section anyway?
                    shaderfloats.WriteValues(material.GetSection<Material.ShaderFloatValues>()?.newdata);

                material.ToBytes();
                material.Save(lastsavefile);
                UseWaitCursor = false;
            }
        }

        private void stringGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            stringGrid.Columns[1].Visible = materials[0]?.GetSectionHeader(Material.SectionType.COMPILEDSHADERS) != null;

            foreach (DataGridViewRow row in stringGrid.Rows)
            {
                if (row.Cells[0].Value == "Template")
                {
                    row.Cells[1].ReadOnly = true;
                    row.Cells[1].Style.BackColor = SystemColors.ControlDark;
                    row.Cells[3].ReadOnly = true;
                    row.Cells[3].Style.BackColor = SystemColors.ControlDark;
                }
                else if (materials[1] != null && ((List<GridEntry>)stringGrid.DataSource)[row.Index].values[GridEntry.SLOTEXTERNAL].Slot == null)
                {
                    row.Cells[2].Style.BackColor = Color.LightPink;
                }
            }
        }


        private void valueGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            valueGrid.Columns[2].Visible = materials[0]?.GetSectionHeader(Material.SectionType.COMPILEDSHADERS) != null;

            foreach (DataGridViewRow row in valueGrid.Rows)
            {
                var v = ((List<GridEntry>)valueGrid.DataSource)[row.Index];
                if (materials[1] != null && v.values[GridEntry.SLOTEXTERNAL].Slot == null)
                {
                    row.Cells[3].Style.BackColor = Color.LightPink;
                }

                if (v.Span > 0f)
                {
                    row.Cells[0].Style.ForeColor = SystemColors.Window;
                    row.Cells[1].Style.ForeColor = SystemColors.Window;
                }
            }
        }

        private void stringGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex == 2)
            {
                statusLabel.Image = global::Spandex.Properties.Resources.warning;
                statusLabel.Text = $"Template path changed.  Save and reopen to apply the new template.";
            }
        }

        private void valueGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var v = ((List<GridEntry>)valueGrid.DataSource)[e.RowIndex];

            if (e.ColumnIndex > 1)
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            else
                switch (v.Span)
                {
                    case null:
                    case 0f:
                        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
                        break;
                }
        }

        private void ValueGrid_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            // back out invalid values here because cell validation is completely broken
            var cell = valueGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value != null && cell.Value is string)
            {
                switch (((List<GridEntry>)valueGrid.DataSource)[e.RowIndex].Type)
                {
                    case GridEntry.TypeOrder.Float:
                        float ftest;
                        cell.Value = float.TryParse((string)cell.Value, out ftest) ? ftest : null; 
                        break;
                    case GridEntry.TypeOrder.Short:
                        ushort utest;
                        cell.Value = ushort.TryParse((string)cell.Value, out utest) ? utest : null; 
                        break;
                    case GridEntry.TypeOrder.Byte:
                        byte btest;
                        cell.Value = byte.TryParse((string)cell.Value, out btest) ? btest : null; 
                        break;
                    case GridEntry.TypeOrder.Integer:
                        uint ui32test;
                        cell.Value = uint.TryParse((string)cell.Value, out ui32test) ? ui32test : null; 
                        break;
                }
            }
        }
    }

    public class GridEntry
    {
        public const int SLOTINTERNAL = 0;
        public const int SLOTOVERRIDE = 1;
        public const int SLOTEXTERNAL = 2;

        private uint id;
        public float? Span { get; set; }
        public uint ID
        {
            get { return id; }
            set
            {
                if (value == 0)
                    IDdisplay = "Template";
                else
                    IDdisplay = Enum.IsDefined(typeof(KnownTextureIDs), value) ? $"{((KnownTextureIDs)value).ToString()} ({value:X8})" : $"{value:X8}";
                id = value;
            }
        }
        public string IDdisplay { get; set; }

        public Values[] values;
        public TypeOrder? Type { get; set; }
        public object Value { get { return values[SLOTOVERRIDE].Value; } set { values[SLOTOVERRIDE].Value = value; } }
        public object InternalValue { get { return values[SLOTINTERNAL].Value; } set { values[SLOTINTERNAL].Value = value; } }
        public object TemplateValue { get { return values[SLOTEXTERNAL].Value; } set { values[SLOTEXTERNAL].Value = value; } }

        public GridEntry()
        {
            values = new Values[3] { new Values(), new Values(), new Values()};
        }

        public enum TypeOrder : uint
        {
            Template = 0,

            // definitions
            Float = 30,
            Short = 31,
            Byte = 32,
            Integer = 39,
        }

        public class Values
        {
            public uint? Slot { get; set; }
            public object Value { get; set; }
        }
    }
}
