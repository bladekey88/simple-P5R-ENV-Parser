using ENVParser.Utils;

namespace ENVParser.ENVFileComponents
{
    internal class PhysicsEffects : IEnvFileSection<PhysicsEffects>
    {
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

        public PhysicsEffects Read(BigEndianBinaryReader reader)
        {
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
            return this;
        }
    }
}
