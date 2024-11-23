using ENVParser.ENVFileComponents;
using ENVParser.Fields;
using ENVParser.Utils;
using ENVParser.Utils.Interfaces;
using System.Reflection;

namespace ENVParser
{

    [Serializable]
    internal class EnvFile : IKeyValueEnumerable
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
        internal static readonly string _sectionValue = "";

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

            //DEPRECATED TO BE REMOVED
            // Populate the property map
            //foreach (var property in typeof(EnvFile).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            //{
            //    _propertyMap[property.Name] = property;
            //}
        }

        public void Add(Dictionary<string, object> dataObject)
        // This hopefully will be generic may only be the JSON implementation
        {
            foreach (var entry in dataObject)
            {
                // Get the property of the current object that corresponds to the key
                // Essentially the component class
                var rootProperty = this.GetType().GetProperty(entry.Key);

                if (rootProperty != null)
                {
                    // We want to get the actual instance object not the properties of it
                    var rootObject = rootProperty.GetValue(this);

                    // Need to explicitly set this to a dictionary so we can iterate (rather than generic object)
                    Dictionary<string, object> propertyFields = (Dictionary<string, object>)entry.Value;

                    foreach (var kvp in propertyFields)
                    {
                        // Get the name and value of the current field
                        string propertyName = kvp.Key;
                        object propertyValue = kvp.Value;

                        // Get the PropertyInfo object for the current property field
                        // This ensures we can map 1:1 from the deserialised json to the object field
                        PropertyInfo propertyInfo = rootObject.GetType().GetProperty(propertyName);

                        if (propertyInfo != null)
                        {
                            // Convert the property value to the correct type - deriving it from the data
                            object convertedValue = Convert.ChangeType(propertyValue, propertyInfo.PropertyType);

                            propertyInfo.SetValue(rootObject, convertedValue);
                        }
                    }
                }
            }
            this.GFSVersion = this.EnvHeader.GFSVersion;
            this.GameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);
        }

        public EnvFile Read(BigEndianBinaryReader reader)
        {
            EnvHeader.Read(reader);
            // Derive GameVersion on read
            this.GFSVersion = this.EnvHeader.GFSVersion;
            this.GameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);

            // Read Content Sections from File
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
            this.GFSVersion = this.EnvHeader.GFSVersion;
            this.GameVersion = ValidVersionHeaderProvider.CheckValidVersion(GFSVersion);

            EnvHeader.Write(writer);
            FieldModelLight0.Write(writer);
            FieldModelLight1.Write(writer);
            FieldModelLight2.Write(writer);
            CharacterModelLight.Write(writer, GFSVersion, GameVersion);
            Fog.Write(writer);
            GlobalLightingEffects.Write(writer, GFSVersion, GameVersion);
            UnknownEffects.Write(writer, GFSVersion, GameVersion);
            FieldShadows.Write(writer);
            FieldShadowColours.Write(writer, GFSVersion, GameVersion);
            ColourCorrections.Write(writer);
            SecondUnknownEffects.Write(writer, GFSVersion, GameVersion);
            Physics.Write(writer);
            ClearColours.Write(writer);
            EnvFooter.Write(writer, GFSVersion, GameVersion);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {

            {
                yield return new KeyValuePair<string, object>("ENV Header", _sectionValue);
                foreach (var component in EnvHeader) { yield return component; }

                yield return new KeyValuePair<string, object>("Field Model Light 0", _sectionValue);
                foreach (var component in FieldModelLight0) { yield return component; }

                yield return new KeyValuePair<string, object>("Field Model Light 1", _sectionValue);
                foreach (var component in FieldModelLight1) { yield return component; }

                yield return new KeyValuePair<string, object>("Field Model Light 2", _sectionValue);
                foreach (var component in FieldModelLight2) { yield return component; }

                yield return new KeyValuePair<string, object>("Character Model Light", _sectionValue);
                foreach (var component in CharacterModelLight) { yield return component; }

                yield return new KeyValuePair<string, object>("Fog", _sectionValue);
                foreach (var component in Fog) { yield return component; }

                yield return new KeyValuePair<string, object>("Global Lighting Effects", _sectionValue);
                foreach (var component in GlobalLightingEffects) { yield return component; }

                yield return new KeyValuePair<string, object>("Unknown Section", _sectionValue);
                foreach (var component in UnknownEffects) { yield return component; }

                yield return new KeyValuePair<string, object>("Field Shadows", _sectionValue);
                foreach (var component in FieldShadows) { yield return component; }

                yield return new KeyValuePair<string, object>("Field Shadows Colour", _sectionValue);
                foreach (var component in FieldShadowColours) { yield return component; }

                yield return new KeyValuePair<string, object>("Colour Corrections", _sectionValue);
                foreach (var component in ColourCorrections) { yield return component; }

                yield return new KeyValuePair<string, object>("Second Unknown Section", _sectionValue);
                foreach (var component in SecondUnknownEffects) { yield return component; }

                yield return new KeyValuePair<string, object>("Physics", _sectionValue);
                foreach (var component in Physics) { yield return component; }

                yield return new KeyValuePair<string, object>("Clear Colours", _sectionValue);
                foreach (var component in ClearColours) { yield return component; }

                yield return new KeyValuePair<string, object>("ENV Footer", _sectionValue);
                foreach (var component in EnvFooter) { yield return component; }
            }
        }
    }


    internal class EnvFileWrapper : IKeyValueEnumerable
    {
        private readonly EnvFile _envFile;

        public EnvFileWrapper(EnvFile envFile)
        {
            _envFile = envFile;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var pair in _envFile)
            {
                // Add any additional logic here, e.g., filtering, transformation
                yield return pair;
            }
        }
    }

}
