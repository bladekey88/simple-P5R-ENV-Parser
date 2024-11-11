using ENVParser.Fields;
using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal sealed class GlobalLighting : IEnvFileSectionVersionSpecific<GlobalLighting>
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
    }
}
