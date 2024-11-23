using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class CharacterModelLighting : BaseEnvSection, IEnvFileSectionVersionSpecific<CharacterModelLighting>
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

        public CharacterModelLighting Write(BigEndianBinaryWriter writer, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GFSVersion >= 17842768)
            {
                writer.Write(LightType);
                writer.Write(EnableCharacterModelSection);
                writer.Write(CharacterModelAmbientRed);
                writer.Write(CharacterModelAmbientGreen);
                writer.Write(CharacterModelAmbientBlue);
                writer.Write(CharacterModelAmbientAlpha);
                writer.Write(CharacterModelDiffuseRed);
                writer.Write(CharacterModelDiffuseGreen);
                writer.Write(CharacterModelDiffuseBlue);
                writer.Write(CharacterModelDiffuseAlpha);
                writer.Write(CharacterModelSpecularRed);
                writer.Write(CharacterModelSpecularGreen);
                writer.Write(CharacterModelSpecularBlue);
                writer.Write(CharacterModelSpecularAlpha);
                writer.Write(CharacterModelEmissiveRed);
                writer.Write(CharacterModelEmissiveGreen);
                writer.Write(CharacterModelEmissiveBlue);
                writer.Write(CharacterModelEmissiveAlpha);
                writer.Write(Field16C);
                writer.Write(Field170);
                writer.Write(Field174);
                writer.Write(Field178);
                writer.Write(CharacterModelLightX);
                writer.Write(CharacterModelLightY);
                writer.Write(CharacterModelLightZ);
            }
            writer.Write(Field188);
            writer.Write(ModelNearClip);
            writer.Write(ModelFarClip);
            return this;
        }
    }
}
