using Spiderman;

namespace Spandex
{
    public partial class Form1 : Form
    {
        public Material material;
        public List<StringGridEntry> strings { get; set; }
        string lastsourcedir, lastoutputdir, lastsavefile;

        public Form1()
        {
            InitializeComponent();
        }


        private void openbutton_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            f.Filter = "Material asset|*.material;*.materialgraph";
            if (lastsourcedir is not null)
                f.InitialDirectory = lastsourcedir;
            savebutton.Enabled = false;
            strings = new List<StringGridEntry>();

            if (f.ShowDialog() == DialogResult.OK)
            {
                UseWaitCursor = true;
                Application.DoEvents();

                lastsourcedir = Path.GetDirectoryName(f.FileName) + @"\";
                material = new Material(f.FileName);
                lastsavefile = Path.ChangeExtension(Path.GetFileName(material.assetfile), $".modified{Path.GetExtension(material.assetfile)}");
                savebutton.Enabled = true;


                var tl = material.GetSection<Material.TextureDefinitions>();
                if (tl == null)
                {
                    if (material.segments.Length > 0)
                    {
                        (var segstrings, var segoffsets) = Material.UnpackStrings(material.segments[0].data);
                        if (segstrings.Count == 1)
                            strings.Add(new StringGridEntry("Template", 0, 0, segstrings[0]));
                    }
                }
                else if (tl.entries.Length > 0)
                {
                    var dataseg = material.sectionlayout.GetSectionByOffset(tl.entries[0].nameoffset);
                    if (dataseg != null)
                    {
                        (var segstrings, var segoffsets) = Material.UnpackStrings(dataseg.data);
                        if (segoffsets.Where(s => s != 0).SequenceEqual(tl.entries.Select(s => s.nameoffset - dataseg.offset).Where(s => s != 0)))
                        {
                            if (dataseg is Material.Headerless)
                                ((Material.Headerless)dataseg).moveable = true;

                            if (tl.entries[0].nameoffset != dataseg.offset)
                                strings.Add(new StringGridEntry("Template", 0, 0, segstrings[0]));

                            for (int i = 0; i < tl.entries.Length; i++)
                            {
                                if (tl.entries[i].nameoffset != segoffsets[strings.Count] + dataseg.offset)
                                    throw new Exception("Parsing assumption failure");
                                strings.Add(new StringGridEntry(Material.TextureDefinitions.displayname, (uint)i, tl.entries[i].u3, segstrings[strings.Count]));
                            }
                        }
                    }
                }

                var material8 = material.GetSection<Material.ShaderOverrides>();
                if (material8 != null)
                {
                    for (uint i = 0; i < material8.strings.Count; i++)
                    {
                        strings.Add(new StringGridEntry(Material.ShaderOverrides.displayname, i, material8.ids[(int)i], material8.strings[(int)i]));
                    }
                }

                UseWaitCursor = false;
            }

            stringGrid.DataSource = strings;
            stringGrid.AutoResizeColumns();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
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
                material = new Material(material.assetfile);

                var tl = material.GetSection<Material.TextureDefinitions>();
                var datastrings = new List<string>();
                var material8 = material.GetSection<Material.ShaderOverrides>();
                var m8idx = 0;
                Material.SectionHeader dataseg = null;
                foreach (var s in strings)
                {
                    switch (s.Type)
                    {
                        case "Template":
                            datastrings.Add(s.Path ?? String.Empty);
                            dataseg = material.segments[0];
                            break;
                        case Material.TextureDefinitions.displayname:
                            datastrings.Add(s.Path ?? String.Empty);
                            break;
                        case Material.ShaderOverrides.displayname:
                            material8.strings[m8idx++] = s.Path ?? String.Empty;
                            break;
                    }
                }

                if (tl == null)
                    (dataseg.newdata, var offsets) = Material.PackStrings(datastrings);
                else if (tl.entries.Length > 0)
                {
                    dataseg = material.sectionlayout.GetSectionByOffset(tl.entries[0].nameoffset);
                    (dataseg.newdata, var offsets) = Material.PackStrings(datastrings);
                    offsets = offsets.Skip(offsets.Length - tl.entries.Length).ToArray();
                    for (int i = 0; i < tl.entries.Length; i++)
                        tl.entries[i].nameoffset = offsets[i] + dataseg.offset;
                }

                material.ToBytes();
                material.Save(lastsavefile);
                UseWaitCursor = false;
            }
        }
    }


    public class StringGridEntry
    {
        public string Type { get; set; }
        public uint Slot { get; set; }
        public uint ID { get; set; }
        public string Path { get; set; }

        public StringGridEntry(string Type, uint Slot, uint ID, string Path)
        {
            this.Type = Type;
            this.Slot = Slot;
            this.ID = ID;
            this.Path = Path;
        }
    }
}
