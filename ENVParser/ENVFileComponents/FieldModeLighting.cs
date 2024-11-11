using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class FieldModeLighting : IEnvFileSection<FieldModeLighting>
    {
        public byte LightType { get; set; }
        public bool EnableFieldModelSection { get; set; }
        public float FieldModelAmbientRed { get; set; }
        public float FieldModelAmbientGreen { get; set; }
        public float FieldModelAmbientBlue { get; set; }
        public float FieldModelAmbientAlpha { get; set; }
        public float FieldModelDiffuseRed { get; set; }
        public float FieldModelDiffuseGreen { get; set; }
        public float FieldModelDiffuseBlue { get; set; }
        public float FieldModelDiffuseAlpha { get; set; }
        public float FieldModelSpecularRed { get; set; }
        public float FieldModelSpecularGreen { get; set; }
        public float FieldModelSpecularBlue { get; set; }
        public float FieldModelSpecularAlpha { get; set; }
        public float FieldModelEmissiveRed { get; set; }
        public float FieldModelEmissiveGreen { get; set; }
        public float FieldModelEmissiveBlue { get; set; }
        public float FieldModelEmissiveAlpha { get; set; }
        public float Field52 { get; set; }
        public float Field56 { get; set; }
        public float Field5A { get; set; }
        public float Field5E { get; set; }
        public float FieldModelLightX { get; set; }
        public float FieldModelLightY { get; set; }
        public float FieldModelLightZ { get; set; }

        public FieldModeLighting Read(BigEndianBinaryReader reader)
        {

            LightType = reader.ReadByte();
            EnableFieldModelSection = reader.ReadBoolean();
            FieldModelAmbientRed = reader.ReadSingle();
            FieldModelAmbientGreen = reader.ReadSingle();
            FieldModelAmbientBlue = reader.ReadSingle();
            FieldModelAmbientAlpha = reader.ReadSingle();
            FieldModelDiffuseRed = reader.ReadSingle();
            FieldModelDiffuseGreen = reader.ReadSingle();
            FieldModelDiffuseBlue = reader.ReadSingle();
            FieldModelDiffuseAlpha = reader.ReadSingle();
            FieldModelSpecularRed = reader.ReadSingle();
            FieldModelSpecularGreen = reader.ReadSingle();
            FieldModelSpecularBlue = reader.ReadSingle();
            FieldModelSpecularAlpha = reader.ReadSingle();
            FieldModelEmissiveRed = reader.ReadSingle();
            FieldModelEmissiveGreen = reader.ReadSingle();
            FieldModelEmissiveBlue = reader.ReadSingle();
            FieldModelEmissiveAlpha = reader.ReadSingle();
            Field52 = reader.ReadSingle();
            Field56 = reader.ReadSingle();
            Field5A = reader.ReadSingle();
            Field5E = reader.ReadSingle();
            FieldModelLightX = reader.ReadSingle();
            FieldModelLightY = reader.ReadSingle();
            FieldModelLightZ = reader.ReadSingle();
            return this;
        }
    }
}
