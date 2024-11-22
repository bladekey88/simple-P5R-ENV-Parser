using ENVParser.Utils;
using ENVParser.Utils.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace ENVParser.ENVFileComponents
{
    internal sealed class FieldModeLighting : BaseEnvSection,  IEnvFileSection<FieldModeLighting>
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

        public FieldModeLighting Write(BigEndianBinaryWriter writer)
        {
            writer.Write(LightType);
            writer.Write(EnableFieldModelSection);
            writer.Write(FieldModelAmbientRed);
            writer.Write(FieldModelAmbientGreen);
            writer.Write(FieldModelAmbientBlue);
            writer.Write(FieldModelAmbientAlpha);
            writer.Write(FieldModelDiffuseRed);
            writer.Write(FieldModelDiffuseGreen);
            writer.Write(FieldModelDiffuseBlue);
            writer.Write(FieldModelDiffuseAlpha);
            writer.Write(FieldModelSpecularRed);
            writer.Write(FieldModelSpecularGreen);
            writer.Write(FieldModelSpecularBlue);
            writer.Write(FieldModelSpecularAlpha);
            writer.Write(FieldModelEmissiveRed);
            writer.Write(FieldModelEmissiveGreen);
            writer.Write(FieldModelEmissiveBlue);
            writer.Write(FieldModelEmissiveAlpha);
            writer.Write(Field52);
            writer.Write(Field56);
            writer.Write(Field5A);
            writer.Write(Field5E);
            writer.Write(FieldModelLightX);
            writer.Write(FieldModelLightY);
            writer.Write(FieldModelLightZ);
            return this;
       
        }
    }
}
