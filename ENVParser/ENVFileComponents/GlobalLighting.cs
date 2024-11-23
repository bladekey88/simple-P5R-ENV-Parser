using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class GlobalLighting : BaseEnvSection, IEnvFileSectionVersionSpecific<GlobalLighting>
    {
        public bool EnableGraphicOutput { get; set; }
        public bool EnableBloom { get; set; }
        public bool EnableGlare { get; set; }
        public bool Field1CC { get; set; }
        public bool Field1CD { get; set; }
        public bool Field1CE { get; set; }
        public bool Field1CF { get; set; }
        public uint Field1D0 { get; set; }
        public float BloomAmount { get; set; }
        public float BloomDetail { get; set; }
        public float BloomWhiteLevel { get; set; }
        public float BloomDarkLevel { get; set; }
        public float GlareSensitivity { get; set; }
        public float SceneWhiteLevels { get; set; }
        public float SceneDarkLevels { get; set; }
        public float Field1F0 { get; set; }
        public float Field1F4 { get; set; }
        public float Field1F8 { get; set; }
        public uint Field1FC { get; set; }
        public float Field200 { get; set; }
        public float Field204 { get; set; }
        public uint Field208 { get; set; }
        public uint Field20C { get; set; }
        public float Field210 { get; set; }
        public float Field214 { get; set; }
        public float RedColourBoost { get; set; }
        public float GreenColourBoost { get; set; }
        public float BlueColourBoost { get; set; }
        public float Field224 { get; set; }
        public float Field228 { get; set; }
        public float Field22C { get; set; }
        public float Field230 { get; set; }
        public float Field234 { get; set; }
        public float Field238 { get; set; }
        public float Field23C { get; set; }
        public float GlareLength { get; set; }
        public float GlareChromaticAberration { get; set; }
        public float GlareDirection { get; set; }
        public uint GlareMode { get; set; }

        public GlobalLighting Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {

            EnableGraphicOutput = reader.ReadBoolean();
            EnableBloom = reader.ReadBoolean();
            EnableGlare = reader.ReadBoolean();
            Field1CC = reader.ReadBoolean();

            // P5R Only Fields 
            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                Field1CD = reader.ReadBoolean();
                Field1CE = reader.ReadBoolean();
                Field1CF = reader.ReadBoolean();
                Field1D0 = reader.ReadUInt32();
            }

            BloomAmount = reader.ReadSingle();
            BloomDetail = reader.ReadSingle();
            BloomWhiteLevel = reader.ReadSingle();
            BloomDarkLevel = reader.ReadSingle();
            GlareSensitivity = reader.ReadSingle();

            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                SceneWhiteLevels = reader.ReadSingle();
                SceneDarkLevels = reader.ReadSingle();
                Field1F0 = reader.ReadSingle();
                Field1F4 = reader.ReadSingle();
                Field1F8 = reader.ReadSingle();
                Field1FC = reader.ReadUInt32();
                Field200 = reader.ReadSingle();
                Field204 = reader.ReadSingle();
                Field208 = reader.ReadUInt32();
                Field20C = reader.ReadUInt32();
                Field210 = reader.ReadSingle();
                Field214 = reader.ReadSingle();
                RedColourBoost = reader.ReadSingle();
                GreenColourBoost = reader.ReadSingle();
                BlueColourBoost = reader.ReadSingle();
                Field224 = reader.ReadSingle();
                Field228 = reader.ReadSingle();
                Field22C = reader.ReadSingle();
                Field230 = reader.ReadSingle();
                Field234 = reader.ReadSingle();
                Field238 = reader.ReadSingle();
                Field23C = reader.ReadSingle();
            }

            if (GFSVersion >= 17843456)
            {
                GlareLength = reader.ReadSingle();
                GlareChromaticAberration = reader.ReadSingle();
                GlareDirection = reader.ReadSingle();
                GlareMode = reader.ReadUInt32();
            }

            return this;
        }

        public GlobalLighting Write(BigEndianBinaryWriter writer, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            writer.Write(EnableGraphicOutput);
            writer.Write(EnableBloom);
            writer.Write(EnableGlare);
            writer.Write(Field1CC);

            // P5R Only Fields
            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                writer.Write(Field1CD);
                writer.Write(Field1CE);
                writer.Write(Field1CF);
                writer.Write(Field1D0);
            }

            writer.Write(BloomAmount);
            writer.Write(BloomDetail);
            writer.Write(BloomWhiteLevel);
            writer.Write(BloomDarkLevel);
            writer.Write(GlareSensitivity);

            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                writer.Write(SceneWhiteLevels);
                writer.Write(SceneDarkLevels);
                writer.Write(Field1F0);
                writer.Write(Field1F4);
                writer.Write(Field1F8);
                writer.Write(Field1FC);
                writer.Write(Field200);
                writer.Write(Field204);
                writer.Write(Field208);
                writer.Write(Field20C);
                writer.Write(Field210);
                writer.Write(Field214);
                writer.Write(RedColourBoost);
                writer.Write(GreenColourBoost);
                writer.Write(BlueColourBoost);
                writer.Write(Field224);
                writer.Write(Field228);
                writer.Write(Field22C);
                writer.Write(Field230);
                writer.Write(Field234);
                writer.Write(Field238);
                writer.Write(Field23C);
            }

            if (GFSVersion >= 17843456)
            {
                writer.Write(GlareLength);
                writer.Write(GlareChromaticAberration);
                writer.Write(GlareDirection);
                writer.Write(GlareMode);
            }

            return this;
        }
    }
}
