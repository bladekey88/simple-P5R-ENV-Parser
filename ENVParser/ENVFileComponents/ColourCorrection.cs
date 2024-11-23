using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class ColourCorrection : BaseEnvSection, IEnvFileSection<ColourCorrection>
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

        public ColourCorrection Write(BigEndianBinaryWriter writer)
        {
            writer.Write(DisplayColourGrading);
            writer.Write(Cyan);
            writer.Write(Magenta);
            writer.Write(Yellow);
            writer.Write(Dodge);
            writer.Write(Burn);
            return this;
        }
    }
}
