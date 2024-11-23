using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class EnvFooter : BaseEnvSection, IEnvFileSectionVersionSpecific<EnvFooter>
    {
        public uint Field324 { get; set; }
        public uint Field328 { get; set; }
        public uint Field32C { get; set; }
        public uint Field330 { get; set; }
        public uint Field334 { get; set; }
        public byte Field338 { get; set; }


        public EnvFooter Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GFSVersion >= 17843968)
            {
                Field324 = reader.ReadUInt32();
            }

            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                Field328 = reader.ReadUInt32();
                Field32C = reader.ReadUInt32();
                Field330 = reader.ReadUInt32();
                Field334 = reader.ReadUInt32();
                Field338 = reader.ReadByte();
            }
            return this;
        }

        public EnvFooter Write(BigEndianBinaryWriter writer, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GFSVersion >= 17843968)
            {
                writer.Write(Field324);
            }

            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                writer.Write(Field328);
                writer.Write(Field32C);
                writer.Write(Field330);
                writer.Write(Field334);
                writer.Write(Field338);
            }
            return this;
        }


    }
}
