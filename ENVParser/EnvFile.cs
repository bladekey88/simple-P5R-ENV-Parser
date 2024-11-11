using ENVParser.ENVFileComponents;
using ENVParser.Fields;
using ENVParser.Utils;
using System.Collections;
using System.Reflection;
namespace ENVParser
{

    [Serializable]
    internal class EnvFile : IEnumerable<KeyValuePair<string, object>>
    {
        public EnvHeader EnvHeader { get; set; }
        public uint GFSVersion { get; set; }

        public ValidVersionHeaderProvider.GameVersions? GameVersion { get; set; }
        public FieldModeLighting FieldModelLight0 { get; set; }
        public FieldModeLighting FieldModelLight1 { get; set; }
        public FieldModeLighting FieldModelLight2 { get; set; }
        public CharacterModelLighting CharacterModelLight { get; set; }
        public EnvironmentFog Fog { get; set; }
        public GlobalLighting GlobalLightingEffects { get; set; }
        public UnknownSection UnknownEffects { get; set; }
        public FieldShadow FieldShadows { get; set; }
        public FieldShadowColour FieldShadowColours { get; set; }
        public ColourCorrection ColourCorrections { get; set; }
        public SecondUnknownSection SecondUnknownEffects { get; set; }
        public PhysicsEffects Physics { get; set; }
        public ClearColour ClearColours { get; set; }
        public EnvFooter EnvFooter { get; set; }


        private readonly Dictionary<string, PropertyInfo> _propertyMap = [];
        private readonly Dictionary<string, object> _envVariables = [];

        public EnvFile()
        {
            // Instantiate Objects
            EnvHeader = new EnvHeader();
            this.GFSVersion = 0;
            this.GameVersion = null;
            FieldModelLight0 = new FieldModeLighting();
            FieldModelLight1 = new FieldModeLighting();
            FieldModelLight2 = new FieldModeLighting();
            CharacterModelLight = new CharacterModelLighting();
            Fog = new EnvironmentFog();
            GlobalLightingEffects = new GlobalLighting();
            UnknownEffects = new UnknownSection();
            FieldShadows = new FieldShadow();
            FieldShadowColours = new FieldShadowColour();
            ColourCorrections = new ColourCorrection();
            SecondUnknownEffects = new SecondUnknownSection();
            Physics = new PhysicsEffects();
            ClearColours = new ClearColour();
            EnvFooter = new EnvFooter();

            //DEPRECATED
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
            EnvHeader.Read(reader);
            // Derive GameVersion on read
            this.GFSVersion = this.EnvHeader.GFSVersion;
            this.GameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);

            // Read Files
            FieldModelLight0.Read(reader);
            FieldModelLight1.Read(reader);
            FieldModelLight2.Read(reader);
            CharacterModelLight.Read(reader, GFSVersion, GameVersion);
            Fog.Read(reader);
            GlobalLightingEffects.Read(reader, GFSVersion, GameVersion);
            UnknownEffects.Read(reader, GFSVersion, GameVersion);
            FieldShadows.Read(reader);
            FieldShadowColours.Read(reader, GFSVersion, GameVersion);
            ColourCorrections.Read(reader);
            SecondUnknownEffects.Read(reader, GFSVersion, GameVersion);
            Physics.Read(reader);
            ClearColours.Read(reader);
            EnvFooter.Read(reader, GFSVersion, GameVersion);

            return this;
        }

        public EnvFile ReadHeader(BigEndianBinaryReader reader)
        {
            EnvHeader.Read(reader);
            // Derive GameVersion on read
            this.GFSVersion = this.EnvHeader.GFSVersion;
            this.GameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);
            return this;
        }

        public void Write(BigEndianBinaryWriter writer)
        {
            //ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);

            //writer.Write(FileMagic);
            //writer.Write(GFSVersion);
            //writer.Write(FileType);
            //writer.Write(Field0C);
            //writer.Write(Field10);
            //writer.Write(EnableFieldModelSection);
            //writer.Write(FieldModelAmbientRed);
            //writer.Write(FieldModelAmbientGreen);
            //writer.Write(FieldModelAmbientBlue);
            //writer.Write(FieldModelAmbientAlpha);
            //writer.Write(FieldModelDiffuseRed);
            //writer.Write(FieldModelDiffuseGreen);
            //writer.Write(FieldModelDiffuseBlue);
            //writer.Write(FieldModelDiffuseAlpha);
            //writer.Write(FieldModelSpecularRed);
            //writer.Write(FieldModelSpecularGreen);
            //writer.Write(FieldModelSpecularBlue);
            //writer.Write(FieldModelSpecularAlpha);
            //writer.Write(FieldModelEmissiveRed);
            //writer.Write(FieldModelEmissiveGreen);
            //writer.Write(FieldModelEmissiveBlue);
            //writer.Write(FieldModelEmissiveAlpha);
            //writer.Write(Field52);
            //writer.Write(Field56);
            //writer.Write(Field5A);
            //writer.Write(Field5E);
            //writer.Write(FieldModelLightX);
            //writer.Write(FieldModelLightY);
            //writer.Write(FieldModelLightZ);
            //writer.Write(UnusedTextureSection);
            //writer.Write(Field12A);
            //writer.Write(EnableCharacterModelSection);
            //writer.Write(CharacterModelAmbientRed);
            //writer.Write(CharacterModelAmbientGreen);
            //writer.Write(CharacterModelAmbientBlue);
            //writer.Write(CharacterModelAmbientAlpha);
            //writer.Write(CharacterModelDiffuseRed);
            //writer.Write(CharacterModelDiffuseGreen);
            //writer.Write(CharacterModelDiffuseBlue);
            //writer.Write(CharacterModelDiffuseAlpha);
            //writer.Write(CharacterModelSpecularRed);
            //writer.Write(CharacterModelSpecularGreen);
            //writer.Write(CharacterModelSpecularBlue);
            //writer.Write(CharacterModelSpecularAlpha);
            //writer.Write(CharacterModelEmissiveRed);
            //writer.Write(CharacterModelEmissiveGreen);
            //writer.Write(CharacterModelEmissiveBlue);
            //writer.Write(CharacterModelEmissiveAlpha);
            //writer.Write(Field16C);
            //writer.Write(Field170);
            //writer.Write(Field174);
            //writer.Write(Field178);
            //writer.Write(CharacterModelLightX);
            //writer.Write(CharacterModelLightY);
            //writer.Write(CharacterModelLightZ);
            //writer.Write(Field188);
            //writer.Write(ModelNearClip);
            //writer.Write(ModelFarClip);
            //writer.Write(EnableFog);
            //writer.Write(EnableAmbientFog);
            //writer.Write(DisableFog);
            //writer.Write(ToggleFogCameraPlane);
            //writer.Write(FogStartDistance);
            //writer.Write(FogEndDistance);
            //writer.Write(FogRed);
            //writer.Write(FogGreen);
            //writer.Write(FogBlue);
            //writer.Write(FogAlpha);
            //writer.Write(EnableFloorFog);
            //writer.Write(FloorFogStartingHeight);
            //writer.Write(FloorFogEndHeight);
            //writer.Write(FloorFogRed);
            //writer.Write(FloorFogGreen);
            //writer.Write(FloorFogBlue);
            //writer.Write(FloorFogOpacity);
            //writer.Write(EnableGraphicOutput);
            //writer.Write(EnableBloom);
            //writer.Write(EnableGlare);
            //writer.Write(Field1CC);

            //// P5R Only Fields
            //if (gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            //{
            //    writer.Write(Field1CD);
            //    writer.Write(Field1CE);
            //    writer.Write(Field1CF);
            //    writer.Write(Field1D0);
            //}

            //writer.Write(BloomAmount);
            //writer.Write(BloomDetail);
            //writer.Write(BloomWhiteLevel);
            //writer.Write(BloomDarkLevel);
            //writer.Write(GlareSensitivity);

            //if (gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            //{
            //    writer.Write(SceneWhiteLevels);
            //    writer.Write(SceneDarkLevels);
            //    writer.Write(Field1F0);
            //    writer.Write(Field1F4);
            //    writer.Write(Field1F8);
            //    writer.Write(Field1FC);
            //    writer.Write(Field200);
            //    writer.Write(Field204);
            //    writer.Write(Field208);
            //    writer.Write(Field20C);
            //    writer.Write(Field210);
            //    writer.Write(Field214);
            //    writer.Write(RedColourBoost);
            //    writer.Write(GreenColourBoost);
            //    writer.Write(BlueColourBoost);
            //    writer.Write(Field224);
            //    writer.Write(Field228);
            //    writer.Write(Field22C);
            //    writer.Write(Field230);
            //    writer.Write(Field234);
            //    writer.Write(Field238);
            //    writer.Write(Field23C);
            //}
            //writer.Write(GlareLength);
            //writer.Write(GlareChromaticAberration);
            //writer.Write(GlareDirection);
            //writer.Write(GlareMode);
            //writer.Write(Field250);
            //writer.Write(Field251);
            //if (gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            //{
            //    writer.Write(Field255);
            //}
            //writer.Write(Field259);
            //writer.Write(Field25D);
            //writer.Write(EnableDOF);
            //writer.Write(DOF_FocalPlane);
            //writer.Write(DOF_NearBlurPlane);
            //writer.Write(DOF_FarBlurPlane);
            //writer.Write(DOF_FarBlurLimit);
            //writer.Write(DOF_BlurScale);
            //writer.Write(DOF_GaussType);
            //writer.Write(EnableSSAO);
            //writer.Write(SSAO_OccluderRadius);
            //writer.Write(SSAO_FallOffRadius);
            //writer.Write(SSAO_BlurScale);
            //writer.Write(SSAO_Brightness);
            //writer.Write(SSAO_DepthRange);
            //writer.Write(DisableUnknownFlaggedSection);
            //writer.Write(FieldShadowFarClip);
            //writer.Write(Field294);
            //writer.Write(AmbientShadowBrightness);
            //writer.Write(Field29C);
            //writer.Write(Field2A0);
            //writer.Write(FieldShadowNearClip);
            //writer.Write(FieldShadowBrightness);
            //writer.Write(Field2AC);
            //writer.Write(Field2AD);
            //writer.Write(Field2AE);
            //writer.Write(Field2AF);
            ////if (gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            ////{
            ////    writer.Write(ShadowColourRed);
            ////    writer.Write(ShadowColourGreen);
            ////    writer.Write(ShadowColourBlue);
            ////    writer.Write(ShadowColourAlpha);
            ////}
            //writer.Write(DisplayColourGrading);
            //writer.Write(Cyan);
            //writer.Write(Magenta);
            //writer.Write(Yellow);
            //writer.Write(Dodge);
            //writer.Write(Burn);
            //writer.Write(LightMapR);
            //writer.Write(LightMapG);
            //writer.Write(LightMapB);
            //writer.Write(LightMapA);
            //writer.Write(EnableOutline);
            //writer.Write(OutlineOpacity);
            //writer.Write(OutlineWidth);
            //writer.Write(CharacterOutlineBrightness);
            //writer.Write(Field2F2);
            //writer.Write(Field2F6);
            //writer.Write(ReflectionHeight);
            //writer.Write(EnablePhysicsSection);
            //writer.Write(Gravity);
            //writer.Write(EnableWind);
            //writer.Write(WindDirectionX);
            //writer.Write(WindDirectionY);
            //writer.Write(WindDirectionZ);
            //writer.Write(WindStrength);
            //writer.Write(WindStrengthModifier);
            //writer.Write(WindCycleTime);
            //writer.Write(WindCycleDelay);
            //writer.Write(ClearColourRed);
            //writer.Write(ClearColourGreen);
            //writer.Write(ClearColourBlue);
            //writer.Write(ClearColourAlpha);
            //writer.Write(Field324);
            //if (gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
            //{
            //    writer.Write(Field328);
            //    writer.Write(Field32C);
            //    writer.Write(Field330);
            //    writer.Write(Field334);
            //    writer.Write(Field338);
            //}
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
