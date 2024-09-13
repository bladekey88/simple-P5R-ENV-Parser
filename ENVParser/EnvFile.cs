using ENVParser.Utils;
using System.Collections;
using System.Reflection;
namespace ENVParser
{

    [Serializable]
    public class EnvFile : IEnumerable<KeyValuePair<string, object>>
    {


        public uint FileMagic { get; set; }
        public uint GFSVersion { get; set; }
        public uint FileType { get; set; }
        public uint Field0C { get; set; }
        public byte Field10 { get; set; }
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
        public byte[] UnusedTextureSection { get; set; } = new byte[188];
        public byte Field12A { get; set; }
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
        public bool EnableFog { get; set; }
        public bool EnableAmbientFog { get; set; }
        public bool DisableFog { get; set; }
        public bool ToggleFogCameraPlane { get; set; }
        public float FogStartDistance { get; set; }
        public float FogEndDistance { get; set; }
        public float FogRed { get; set; }
        public float FogGreen { get; set; }
        public float FogBlue { get; set; }
        public float FogAlpha { get; set; }
        public bool EnableFloorFog { get; set; }
        public float FloorFogStartingHeight { get; set; }
        public float FloorFogEndHeight { get; set; }
        public float FloorFogRed { get; set; }
        public float FloorFogGreen { get; set; }
        public float FloorFogBlue { get; set; }
        public float FloorFogOpacity { get; set; }
        public bool EnableGraphicOutput { get; set; }
        public bool EnableBloom { get; set; }
        public bool EnableGlare { get; set; }
        public uint Field1CC { get; set; }
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
        public float ShadowColourRed { get; set; }
        public float ShadowColourGreen { get; set; }
        public float ShadowColourBlue { get; set; }
        public float ShadowColourAlpha { get; set; }
        public bool DisplayColourGrading { get; set; }
        public float Cyan { get; set; }
        public float Magenta { get; set; }
        public float Yellow { get; set; }
        public float Dodge { get; set; }
        public float Burn { get; set; }
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
        public bool EnablePhysicsSection { get; set; }
        public float Gravity { get; set; }
        public bool EnableWind { get; set; }
        public float WindDirectionX { get; set; }
        public float WindDirectionY { get; set; }
        public float WindDirectionZ { get; set; }
        public float WindStrength { get; set; }
        public float WindStrengthModifier { get; set; }
        public float WindCycleTime { get; set; }
        public float WindCycleDelay { get; set; }
        public byte ClearColourRed { get; set; }
        public byte ClearColourGreen { get; set; }
        public byte ClearColourBlue { get; set; }
        public byte ClearColourAlpha { get; set; }
        public uint Field324 { get; set; }
        public uint Field328 { get; set; }
        public uint Field32C { get; set; }
        public uint Field330 { get; set; }
        public uint Field334 { get; set; }
        public byte Field338 { get; set; }

        private readonly Dictionary<string, PropertyInfo> _propertyMap = new Dictionary<string, PropertyInfo>();
        private readonly Dictionary<string, object> _envVariables = new Dictionary<string, object>();

        public EnvFile()
        {
            // Populate the property map
            foreach (var property in typeof(EnvFile).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                _propertyMap[property.Name] = property;
            }
        }

        public void Add(string key, object value)
        {
            if (_propertyMap.ContainsKey(key))
            {
                PropertyInfo property = _propertyMap[key];
                if (property.CanWrite)
                {
                    property.SetValue(this, value);
                }
            }
            else
            {
                throw new InvalidOperationException($"Unknown Property: {key} with value {value}");
            }
        }
        public EnvFile Read(BigEndianBinaryReader reader)
        {
            FileMagic = reader.ReadUInt32();
            GFSVersion = reader.ReadUInt32();
            FileType = reader.ReadUInt32();
            Field0C = reader.ReadUInt32();
            Field10 = reader.ReadByte();
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
            UnusedTextureSection = reader.ReadBytes(188);
            Field12A = reader.ReadByte();
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
            Field188 = reader.ReadSingle();
            ModelNearClip = reader.ReadSingle();
            ModelFarClip = reader.ReadSingle();
            EnableFog = reader.ReadBoolean();
            EnableAmbientFog = reader.ReadBoolean();
            DisableFog = reader.ReadBoolean();
            ToggleFogCameraPlane = reader.ReadBoolean();
            FogStartDistance = reader.ReadSingle();
            FogEndDistance = reader.ReadSingle();
            FogRed = reader.ReadSingle();
            FogGreen = reader.ReadSingle();
            FogBlue = reader.ReadSingle();
            FogAlpha = reader.ReadSingle();
            EnableFloorFog = reader.ReadBoolean();
            FloorFogStartingHeight = reader.ReadSingle();
            FloorFogEndHeight = reader.ReadSingle();
            FloorFogRed = reader.ReadSingle();
            FloorFogGreen = reader.ReadSingle();
            FloorFogBlue = reader.ReadSingle();
            FloorFogOpacity = reader.ReadSingle();
            EnableGraphicOutput = reader.ReadBoolean();
            EnableBloom = reader.ReadBoolean();
            EnableGlare = reader.ReadBoolean();
            Field1CC = reader.ReadUInt32();
            Field1D0 = reader.ReadUInt32();
            BloomAmount = reader.ReadSingle();
            BloomDetail = reader.ReadSingle();
            BloomWhiteLevel = reader.ReadSingle();
            BloomDarkLevel = reader.ReadSingle();
            GlareSensitivity = reader.ReadSingle();
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
            GlareLength = reader.ReadSingle();
            GlareChromaticAberration = reader.ReadSingle();
            GlareDirection = reader.ReadSingle();
            GlareMode = reader.ReadUInt32();
            Field250 = reader.ReadBoolean();
            Field251 = reader.ReadUInt32();
            Field255 = reader.ReadSingle();
            Field259 = reader.ReadSingle();
            Field25D = reader.ReadSingle();
            EnableDOF = reader.ReadBoolean();
            DOF_FocalPlane = reader.ReadSingle();
            DOF_NearBlurPlane = reader.ReadSingle();
            DOF_FarBlurPlane = reader.ReadSingle();
            DOF_FarBlurLimit = reader.ReadSingle();
            DOF_BlurScale = reader.ReadSingle();
            DOF_GaussType = reader.ReadUInt32();
            EnableSSAO = reader.ReadBoolean();
            SSAO_OccluderRadius = reader.ReadSingle();
            SSAO_FallOffRadius = reader.ReadSingle();
            SSAO_BlurScale = reader.ReadSingle();
            SSAO_Brightness = reader.ReadSingle();
            SSAO_DepthRange = reader.ReadSingle();
            DisableUnknownFlaggedSection = reader.ReadBoolean();
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
            ShadowColourRed = reader.ReadSingle();
            ShadowColourGreen = reader.ReadSingle();
            ShadowColourBlue = reader.ReadSingle();
            ShadowColourAlpha = reader.ReadSingle();
            DisplayColourGrading = reader.ReadBoolean();
            Cyan = reader.ReadSingle();
            Magenta = reader.ReadSingle();
            Yellow = reader.ReadSingle();
            Dodge = reader.ReadSingle();
            Burn = reader.ReadSingle();
            LightMapR = reader.ReadSingle();
            LightMapG = reader.ReadSingle();
            LightMapB = reader.ReadSingle();
            LightMapA = reader.ReadSingle();
            EnableOutline = reader.ReadBoolean();
            OutlineOpacity = reader.ReadSingle();
            OutlineWidth = reader.ReadSingle();
            CharacterOutlineBrightness = reader.ReadSingle();
            Field2F2 = reader.ReadSingle();
            Field2F6 = reader.ReadSingle();
            ReflectionHeight = reader.ReadSingle();
            EnablePhysicsSection = reader.ReadBoolean();
            Gravity = reader.ReadSingle();
            EnableWind = reader.ReadBoolean();
            WindDirectionX = reader.ReadSingle();
            WindDirectionY = reader.ReadSingle();
            WindDirectionZ = reader.ReadSingle();
            WindStrength = reader.ReadSingle();
            WindStrengthModifier = reader.ReadSingle();
            WindCycleTime = reader.ReadSingle();
            WindCycleDelay = reader.ReadSingle();
            ClearColourRed = reader.ReadByte();
            ClearColourGreen = reader.ReadByte();
            ClearColourBlue = reader.ReadByte();
            ClearColourAlpha = reader.ReadByte();
            Field324 = reader.ReadUInt32();
            Field328 = reader.ReadUInt32();
            Field32C = reader.ReadUInt32();
            Field330 = reader.ReadUInt32();
            Field334 = reader.ReadUInt32();
            Field338 = reader.ReadByte();

            return this;
        }

        public void Write(BigEndianBinaryWriter writer)
        {
            writer.Write(FileMagic);
            writer.Write(GFSVersion);
            writer.Write(FileType);
            writer.Write(Field0C);
            writer.Write(Field10);
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
            writer.Write(UnusedTextureSection);
            writer.Write(Field12A);
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
            writer.Write(Field188);
            writer.Write(ModelNearClip);
            writer.Write(ModelFarClip);
            writer.Write(EnableFog);
            writer.Write(EnableAmbientFog);
            writer.Write(DisableFog);
            writer.Write(ToggleFogCameraPlane);
            writer.Write(FogStartDistance);
            writer.Write(FogEndDistance);
            writer.Write(FogRed);
            writer.Write(FogGreen);
            writer.Write(FogBlue);
            writer.Write(FogAlpha);
            writer.Write(EnableFloorFog);
            writer.Write(FloorFogStartingHeight);
            writer.Write(FloorFogEndHeight);
            writer.Write(FloorFogRed);
            writer.Write(FloorFogGreen);
            writer.Write(FloorFogBlue);
            writer.Write(FloorFogOpacity);
            writer.Write(EnableGraphicOutput);
            writer.Write(EnableBloom);
            writer.Write(EnableGlare);
            writer.Write(Field1CC);
            writer.Write(Field1D0);
            writer.Write(BloomAmount);
            writer.Write(BloomDetail);
            writer.Write(BloomWhiteLevel);
            writer.Write(BloomDarkLevel);
            writer.Write(GlareSensitivity);
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
            writer.Write(GlareLength);
            writer.Write(GlareChromaticAberration);
            writer.Write(GlareDirection);
            writer.Write(GlareMode);
            writer.Write(Field250);
            writer.Write(Field251);
            writer.Write(Field255);
            writer.Write(Field259);
            writer.Write(Field25D);
            writer.Write(EnableDOF);
            writer.Write(DOF_FocalPlane);
            writer.Write(DOF_NearBlurPlane);
            writer.Write(DOF_FarBlurPlane);
            writer.Write(DOF_FarBlurLimit);
            writer.Write(DOF_BlurScale);
            writer.Write(DOF_GaussType);
            writer.Write(EnableSSAO);
            writer.Write(SSAO_OccluderRadius);
            writer.Write(SSAO_FallOffRadius);
            writer.Write(SSAO_BlurScale);
            writer.Write(SSAO_Brightness);
            writer.Write(SSAO_DepthRange);
            writer.Write(DisableUnknownFlaggedSection);
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
            writer.Write(ShadowColourRed);
            writer.Write(ShadowColourGreen);
            writer.Write(ShadowColourBlue);
            writer.Write(ShadowColourAlpha);
            writer.Write(DisplayColourGrading);
            writer.Write(Cyan);
            writer.Write(Magenta);
            writer.Write(Yellow);
            writer.Write(Dodge);
            writer.Write(Burn);
            writer.Write(LightMapR);
            writer.Write(LightMapG);
            writer.Write(LightMapB);
            writer.Write(LightMapA);
            writer.Write(EnableOutline);
            writer.Write(OutlineOpacity);
            writer.Write(OutlineWidth);
            writer.Write(CharacterOutlineBrightness);
            writer.Write(Field2F2);
            writer.Write(Field2F6);
            writer.Write(ReflectionHeight);
            writer.Write(EnablePhysicsSection);
            writer.Write(Gravity);
            writer.Write(EnableWind);
            writer.Write(WindDirectionX);
            writer.Write(WindDirectionY);
            writer.Write(WindDirectionZ);
            writer.Write(WindStrength);
            writer.Write(WindStrengthModifier);
            writer.Write(WindCycleTime);
            writer.Write(WindCycleDelay);
            writer.Write(ClearColourRed);
            writer.Write(ClearColourGreen);
            writer.Write(ClearColourBlue);
            writer.Write(ClearColourAlpha);
            writer.Write(Field324);
            writer.Write(Field328);
            writer.Write(Field32C);
            writer.Write(Field330);
            writer.Write(Field334);
            writer.Write(Field338);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var prop in typeof(EnvFile).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(this));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); // Calls the generic version
        }
    }
}
