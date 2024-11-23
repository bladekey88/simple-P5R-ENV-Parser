using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal class SecondUnknownSection : BaseEnvSection, IEnvFileSectionVersionSpecific<SecondUnknownSection>
    {
        public float LightMapR { get; set; }
        public float LightMapG { get; set; }
        public float LightMapB { get; set; }
        public float LightMapA { get; set; }
        public bool EnableOutline { get; set; }
        public float OutlineOpacity { get; set; }
        public float OutlineWidth { get; set; }
        public float CharacterOutlineBrightness { get; set; }
        public float Field2F2 { get; set; }
        public float Field2F6 { get; set; }
        public float ReflectionHeight { get; set; }

        public SecondUnknownSection Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            LightMapR = reader.ReadSingle();
            LightMapG = reader.ReadSingle();
            LightMapB = reader.ReadSingle();
            LightMapA = reader.ReadSingle();
            EnableOutline = reader.ReadBoolean();
            OutlineOpacity = reader.ReadSingle();
            OutlineWidth = reader.ReadSingle();

            if (GFSVersion >= 17844592)
            {
                CharacterOutlineBrightness = reader.ReadSingle();
            }

            if (GFSVersion >= 17846320)
            {

                Field2F2 = reader.ReadSingle();
                Field2F6 = reader.ReadSingle();
            }
            ReflectionHeight = reader.ReadSingle();

            return this;
        }

        public SecondUnknownSection Write(BigEndianBinaryWriter writer, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            writer.Write(LightMapR);
            writer.Write(LightMapG);
            writer.Write(LightMapB);
            writer.Write(LightMapA);
            writer.Write(EnableOutline);
            writer.Write(OutlineOpacity);
            writer.Write(OutlineWidth);

            if (GFSVersion >= 17844592)
            {
                writer.Write(CharacterOutlineBrightness);
            }

            if (GFSVersion >= 17846320)
            {
                writer.Write(Field2F2);
                writer.Write(Field2F6);
            }
            writer.Write(ReflectionHeight);
            return this;
        }
    }
}
