using ENVParser.Fields;
using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class CharacterModelLighting : IEnvFileSectionVersionSpecific<CharacterModelLighting>
    {
        public byte LightType { get; set; }
        public bool EnableCharacterModelSection { get; set; }
        public float CharacterModelAmbientRed { get; set; }
        public float CharacterModelAmbientGreen { get; set; }
        public float CharacterModelAmbientBlue { get; set; }
        public float CharacterModelAmbientAlpha { get; set; }
        public float CharacterModelDiffuseRed { get; set; }
        public float CharacterModelDiffuseGreen { get; set; }
        public float CharacterModelDiffuseBlue { get; set; }
        public float CharacterModelDiffuseAlpha { get; set; }
        public float CharacterModelSpecularRed { get; set; }
        public float CharacterModelSpecularGreen { get; set; }
        public float CharacterModelSpecularBlue { get; set; }
        public float CharacterModelSpecularAlpha { get; set; }
        public float CharacterModelEmissiveRed { get; set; }
        public float CharacterModelEmissiveGreen { get; set; }
        public float CharacterModelEmissiveBlue { get; set; }
        public float CharacterModelEmissiveAlpha { get; set; }
        public float Field16C { get; set; }
        public float Field170 { get; set; }
        public float Field174 { get; set; }
        public float Field178 { get; set; }
        public float CharacterModelLightX { get; set; }
        public float CharacterModelLightY { get; set; }
        public float CharacterModelLightZ { get; set; }
        public float Field188 { get; set; }
        public float ModelNearClip { get; set; }
        public float ModelFarClip { get; set; }

        public CharacterModelLighting Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GFSVersion >= 17842768)
            {
                LightType = reader.ReadByte();
                EnableCharacterModelSection = reader.ReadBoolean();
                CharacterModelAmbientRed = reader.ReadSingle();
                CharacterModelAmbientGreen = reader.ReadSingle();
                CharacterModelAmbientBlue = reader.ReadSingle();
                CharacterModelAmbientAlpha = reader.ReadSingle();
                CharacterModelDiffuseRed = reader.ReadSingle();
                CharacterModelDiffuseGreen = reader.ReadSingle();
                CharacterModelDiffuseBlue = reader.ReadSingle();
                CharacterModelDiffuseAlpha = reader.ReadSingle();
                CharacterModelSpecularRed = reader.ReadSingle();
                CharacterModelSpecularGreen = reader.ReadSingle();
                CharacterModelSpecularBlue = reader.ReadSingle();
                CharacterModelSpecularAlpha = reader.ReadSingle();
                CharacterModelEmissiveRed = reader.ReadSingle();
                CharacterModelEmissiveGreen = reader.ReadSingle();
                CharacterModelEmissiveBlue = reader.ReadSingle();
                CharacterModelEmissiveAlpha = reader.ReadSingle();
                Field16C = reader.ReadSingle();
                Field170 = reader.ReadSingle();
                Field174 = reader.ReadSingle();
                Field178 = reader.ReadSingle();
                CharacterModelLightX = reader.ReadSingle();
                CharacterModelLightY = reader.ReadSingle();
                CharacterModelLightZ = reader.ReadSingle();
            }
            Field188 = reader.ReadSingle();
            ModelNearClip = reader.ReadSingle();
            ModelFarClip = reader.ReadSingle();
            return this;
        }
    }
}
