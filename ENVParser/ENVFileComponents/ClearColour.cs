using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class ClearColour : IEnvFileSection<ClearColour>
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
