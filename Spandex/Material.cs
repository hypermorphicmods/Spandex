using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spiderman
{
    public class Material : Asset<Material.SectionType>
    {
        public Material(string filename) : base(filename)
        {
            if (ps4header.type != SectionType.MATERIALFILE && ps4header.type != SectionType.MATERIALTEMPLATE)
                throw new Exception("Not a material or material template file");
        }

        public enum SectionType : uint
        {
            TEXTUREDEFINITION = 0x1CAFE804,
            MATUNK1 = 0x3E45AA13,
            MATUNK2 = 0x45C4F4C0,
            MATUNK3 = 0x8C049CCA,
            MATUNK4 = 0xA59F667B,
            MATUNK5 = 0xBBFC8900,
            MATUNK6 = 0xBC93FB5E,
            MATUNK7 = 0xE1275683,
            SHADEROVERRIDES = 0xF5260180,
            MATUNK9 = 0xF9C35F30,
            MATUNK10 = 0xFD113362,
            MATERIALTEMPLATE1 = 0xC24B19D9,

            // extras
            MATERIALFILE = 0x1C04EF8C,
            MATERIALTEMPLATE = 0x7DC03E3,
        }

        public override Section MapTypeToSection(SectionType sectiontype, byte[] data)
        {
            switch (sectiontype)
            {
                case SectionType.TEXTUREDEFINITION:
                    return new TextureDefinitions(data);
                case SectionType.SHADEROVERRIDES:
                    return new ShaderOverrides(data);
                default:
                    return new Section(data);
            }
        }

        public class TextureDefinitions : Section
        {
            public TextureListEntry[] entries;
            public const string displayname = "Textures";

            public TextureDefinitions(byte[] data) : base(data)
            {
                entries = new TextureListEntry[data.Length / 16];
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i] = new TextureListEntry(data.Skip(i * 16).Take(16).ToArray());
                }
            }

            public override void ExternalDataParser(BinaryReader br)
            {
                foreach (var entry in entries)
                {
                    br.BaseStream.Seek(entry.nameoffset + PS4Header.length, SeekOrigin.Begin);
                    entry.name = ReadStringZ(br);
                }
            }

            public override bool ToBytes()
            {
                newdata = new byte[entries.Length * 16];
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].ToBytes().CopyTo(newdata, i * 16);
                }
                return true;
            }

            public class TextureListEntry
            {
                public uint nameoffset;
                public ushort u1;
                public ushort u2;
                public uint u3;
                public uint u4;
                public string name;

                public TextureListEntry(byte[] data)
                {
                    nameoffset = BitConverter.ToUInt32(data, 0);
                    u1 = BitConverter.ToUInt16(data, 4);
                    u2 = BitConverter.ToUInt16(data, 6);
                    u3 = BitConverter.ToUInt32(data, 8);
                    u4 = BitConverter.ToUInt32(data, 12);
                }

                public byte[] ToBytes()
                {
                    byte[] data = new byte[16];
                    BitConverter.GetBytes(nameoffset).CopyTo(data, 0);
                    BitConverter.GetBytes(u1).CopyTo(data, 4);
                    BitConverter.GetBytes(u2).CopyTo(data, 6);
                    BitConverter.GetBytes(u3).CopyTo(data, 8);
                    BitConverter.GetBytes(u4).CopyTo(data, 12);
                    return data;
                }
            }
        }

        public class ShaderOverrides : Section
        {
            public uint size;
            // only 0 or 1.  seems to doubly define string 0 for a second purpose
            public uint othercount;
            public uint otherdefoffset;
            public uint strcount;
            public uint strdefoffset;
            public uint strdataoffset;
            public uint[] ids;
            public List<string> strings;

            public const string displayname = "Overrides";

            public ShaderOverrides(byte[] data) : base(data)
            {
                size = BitConverter.ToUInt32(data, 0);
                othercount = BitConverter.ToUInt32(data, 0xC);
                otherdefoffset = BitConverter.ToUInt32(data, 0x10);
                strcount = BitConverter.ToUInt32(data, 0x14);
                strdefoffset = BitConverter.ToUInt32(data, 0x18);
                strdataoffset = BitConverter.ToUInt32(data, 0x1C);

                if (strdefoffset + 8 * strcount != strdataoffset)
                    throw new Exception("Overrides assertion failed");

                ids = new uint[strcount];
                uint[] stringoffsets = new uint[strcount];
                strings = new List<string>();

                uint idx = strdefoffset;
                for (int i = 0; i < strcount; i++, idx += 8)
                    stringoffsets[i] = BitConverter.ToUInt32(data, (int)idx);

                (strings, var offsets) = UnpackStrings(data.Skip((int)idx).ToArray(), stringoffsets);
            }

            public override bool ToBytes()
            {
                (byte[] stringdata, uint[] offsets) = PackStrings(strings);
                if (strings.Count != strcount)
                    throw new Exception("Cannot change string count");

                size = strdataoffset + (uint)stringdata.Length;
                for (; size % 16 != 0; size++) { }
                newdata = new byte[size];
                data.Take((int)strdataoffset).ToArray().CopyTo(newdata, 0);
                stringdata.CopyTo(newdata, strdataoffset);
                BitConverter.GetBytes(size).CopyTo(newdata, 0);

                var oidx = strdefoffset;
                for (uint i = 0; i < offsets.Length; i++, oidx += 8)
                    BitConverter.GetBytes(offsets[i]).CopyTo(newdata, oidx);

                return true;
            }
        }
    }
}
