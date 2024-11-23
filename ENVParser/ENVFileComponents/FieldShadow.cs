using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class FieldShadow : BaseEnvSection, IEnvFileSection<FieldShadow>
    {
        public float FieldShadowFarClip { get; set; }
        public float Field294 { get; set; }
        public float AmbientShadowBrightness { get; set; }
        public float Field29C { get; set; }
        public uint Field2A0 { get; set; }
        public float FieldShadowNearClip { get; set; }
        public float FieldShadowBrightness { get; set; }
        public bool Field2AC { get; set; }
        public bool Field2AD { get; set; }
        public bool Field2AE { get; set; }
        public bool Field2AF { get; set; }

        public FieldShadow Read(BigEndianBinaryReader reader)
        {
            FieldShadowFarClip = reader.ReadSingle();
            Field294 = reader.ReadSingle();
            AmbientShadowBrightness = reader.ReadSingle();
            Field29C = reader.ReadSingle();
            Field2A0 = reader.ReadUInt32();
            FieldShadowNearClip = reader.ReadSingle();
            FieldShadowBrightness = reader.ReadSingle();
            Field2AC = reader.ReadBoolean();
            Field2AD = reader.ReadBoolean();
            Field2AE = reader.ReadBoolean();
            Field2AF = reader.ReadBoolean();
            return this;
        }

        public FieldShadow Write(BigEndianBinaryWriter writer)
        {
            writer.Write(FieldShadowFarClip);
            writer.Write(Field294);
            writer.Write(AmbientShadowBrightness);
            writer.Write(Field29C);
            writer.Write(Field2A0);
            writer.Write(FieldShadowNearClip);
            writer.Write(FieldShadowBrightness);
            writer.Write(Field2AC);
            writer.Write(Field2AD);
            writer.Write(Field2AE);
            writer.Write(Field2AF);
            return this;
        }
    }
}
