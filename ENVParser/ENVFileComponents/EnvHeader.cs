using CsvHelper;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;
using System.Collections.Generic;

namespace ENVParser.ENVFileComponents
{
    internal sealed class EnvHeader : BaseEnvSection, IEnvFileSection<EnvHeader>
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

        public EnvHeader Write(BigEndianBinaryWriter writer)
        {
            writer.Write(FileMagic);
            writer.Write(GFSVersion);
            writer.Write(FileType);
            writer.Write(Field0C);
            return this;
        }
    }
}
