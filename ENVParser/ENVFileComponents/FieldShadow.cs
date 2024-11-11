using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class FieldShadow : IEnvFileSection<FieldShadow>
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
    }
}
