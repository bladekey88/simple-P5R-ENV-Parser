using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class ColourCorrection : IEnvFileSection<ColourCorrection>
    {
        public bool DisplayColourGrading { get; set; }
        public float Cyan { get; set; }
        public float Magenta { get; set; }
        public float Yellow { get; set; }
        public float Dodge { get; set; }
        public float Burn { get; set; }

        public ColourCorrection Read(BigEndianBinaryReader reader)
        {
            DisplayColourGrading = reader.ReadBoolean();
            Cyan = reader.ReadSingle();
            Magenta = reader.ReadSingle();
            Yellow = reader.ReadSingle();
            Dodge = reader.ReadSingle();
            Burn = reader.ReadSingle();
            return this;
        }
    }
}
