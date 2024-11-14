using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;

namespace ENVParser.ENVFileComponents
{
    internal sealed class UnknownSection : BaseEnvSection, IEnvFileSectionVersionSpecific<UnknownSection>
    {
        public bool Field250 { get; set; }
        public uint Field251 { get; set; }
        public float Field255 { get; set; }
        public float Field259 { get; set; }
        public float Field25D { get; set; }
        public bool EnableDOF { get; set; }
        public float DOF_FocalPlane { get; set; }
        public float DOF_NearBlurPlane { get; set; }
        public float DOF_FarBlurPlane { get; set; }
        public float DOF_FarBlurLimit { get; set; }
        public float DOF_BlurScale { get; set; }
        public uint DOF_GaussType { get; set; }
        public bool EnableSSAO { get; set; }
        public float SSAO_OccluderRadius { get; set; }
        public float SSAO_FallOffRadius { get; set; }
        public float SSAO_BlurScale { get; set; }
        public float SSAO_Brightness { get; set; }
        public float SSAO_DepthRange { get; set; }
        public bool DisableUnknownFlaggedSection { get; set; }      

        public UnknownSection Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion)
        {
            if (GFSVersion >= 17846352)
            {
                Field250 = reader.ReadBoolean();
                Field251 = reader.ReadUInt32();
            }

            if (GameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            {
                Field255 = reader.ReadSingle();
            }

            if (GFSVersion >= 17846352)
            {
                Field259 = reader.ReadSingle();
                Field25D = reader.ReadSingle();
            }
            EnableDOF = reader.ReadBoolean();
            DOF_FocalPlane = reader.ReadSingle();
            DOF_NearBlurPlane = reader.ReadSingle();
            DOF_FarBlurPlane = reader.ReadSingle();
            DOF_FarBlurLimit = reader.ReadSingle();
            DOF_BlurScale = reader.ReadSingle();

            if (GFSVersion >= 17846288)
            {
                DOF_GaussType = reader.ReadUInt32();
            }
            EnableSSAO = reader.ReadBoolean();
            SSAO_OccluderRadius = reader.ReadSingle();
            SSAO_FallOffRadius = reader.ReadSingle();
            SSAO_BlurScale = reader.ReadSingle();
            SSAO_Brightness = reader.ReadSingle();
            SSAO_DepthRange = reader.ReadSingle();
            DisableUnknownFlaggedSection = reader.ReadBoolean();

            return this;
        }
    }
}
