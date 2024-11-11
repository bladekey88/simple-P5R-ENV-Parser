using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class EnvHeader : IEnvFileSection<EnvHeader>
    {
        public uint FileMagic { get; set; }
        public uint GFSVersion { get; set; }
        public uint FileType { get; set; }
        public uint Field0C { get; set; }

        public EnvHeader Read(BigEndianBinaryReader reader)
        {
            FileMagic = reader.ReadUInt32();
            GFSVersion = reader.ReadUInt32();
            FileType = reader.ReadUInt32();
            Field0C = reader.ReadUInt32();
            return this;
        }
    }
}
