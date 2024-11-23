using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class FieldShadowColour : BaseEnvSection, IEnvFileSectionVersionSpecific<FieldShadowColour>
    {
        public float ShadowColourRed { get; set; }
        public float ShadowColourGreen { get; set; }
        public float ShadowColourBlue { get; set; }
        public float ShadowColourAlpha { get; set; }
      
        public FieldShadowColour Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GameVersion == ValidVersionHeaderProvider.GameVersions.P5Royal)
            {
                ShadowColourRed = reader.ReadSingle();
                ShadowColourGreen = reader.ReadSingle();
                ShadowColourBlue = reader.ReadSingle();
                ShadowColourAlpha = reader.ReadSingle();
            }
            return this;
        }

        public FieldShadowColour Write(BigEndianBinaryWriter writer, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                writer.Write(ShadowColourRed);
                writer.Write(ShadowColourGreen);
                writer.Write(ShadowColourBlue);
                writer.Write(ShadowColourAlpha);
            }
            return this;
        }

    }
}
