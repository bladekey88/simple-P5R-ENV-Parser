using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal class ClearColour : BaseEnvSection, IEnvFileSection<ClearColour>
    {
        public byte ClearColourRed { get; set; }
        public byte ClearColourGreen { get; set; }
        public byte ClearColourBlue { get; set; }
        public byte ClearColourAlpha { get; set; }

        
        public ClearColour Read(BigEndianBinaryReader reader)
        {
            ClearColourRed = reader.ReadByte();
            ClearColourGreen = reader.ReadByte();
            ClearColourBlue = reader.ReadByte();
            ClearColourAlpha = reader.ReadByte();
            return this;
        }
    }
}
