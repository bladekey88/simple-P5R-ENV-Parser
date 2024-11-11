﻿using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal sealed class EnvironmentFog : IEnvFileSection<EnvironmentFog>
    {
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


        public EnvironmentFog Read(BigEndianBinaryReader reader)
        {
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
            return this;
        }


    }
}