using ENVParser.Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ENVParser.Utils
{
    internal class JsonExporter
    {

        private readonly HashSet<string> _validFieldsForRGBValues;
        private readonly JsonOutput output;

        public JsonExporter()
        {
            _validFieldsForRGBValues = RGBFieldNameProvider.GetValidFields();
        }

        public void Export(string filePath, EnvFile envFile)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "An output file path must be supplied.");
            }
            string outputDirectory = Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Failed to get directory name from file path.");

            // Set up sections for json from structs
            //var sections = new List<Section>();
            List<JsonOutput> fields = [];

            foreach (var data in envFile)
            {
                // unpack tuple and save into appropriate fields 

                var (fieldName, fieldValue) = data;
                string fieldTypeInternal = data.Value.GetType().ToString();
                var fieldType = "f32";

                // Use a switch to get friendly type names
                // Enables consistency with ENV Template
                switch (fieldValue)
                {
                    case bool:
                        fieldType = "boolean";
                        break;
                    case UInt32:
                        fieldType = "u32";
                        break;
                    case float:
                        fieldType = "f32";
                        break;
                    case byte:
                        fieldType = "u8";
                        break;
                    default:
                        break;
                }

                // Handle the unused Texture Section separately to everything else
                if (fieldName == "UnusedTextureSection")
                {
                    // Explicit cast here to byte array to allow access to LINQ
                    // Hardcode the type field, since it doesn't change
                    byte[] reserveValues = (byte[])fieldValue;
                    foreach (var val in reserveValues.Select((value, index) => new { value, index }))
                    {
                        JsonOutput output = new()
                        {
                            FieldName = $"Reserve[{val.index}]",
                            FieldValue = JToken.FromObject(val.value),
                            RGBValue = null,
                            FieldType = "u8"
                        };
                        fields.Add(output);
                    }
                }
                else
                {
                    JsonOutput output = new()
                    {
                        FieldName = fieldName,
                        FieldValue = fieldValue != null ? JToken.FromObject(fieldValue) : null,
                        RGBValue = _validFieldsForRGBValues.Contains(fieldName) ? GetRGBValue(fieldValue) : null,
                        FieldType = fieldType
                    };
                    fields.Add(output);
                }
            }

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string json = JsonConvert.SerializeObject(fields, Formatting.Indented, settings);
            using StreamWriter sw = new(filePath);
            {
                sw.Write(json);
            }
        }

        // Defines the structure of JSON records
        private class JsonOutput
        {
            public string FieldName { get; set; }
            public JToken? FieldValue { get; set; }
            public float? RGBValue { get; set; }
            public string FieldType { get; set; }
        }

        private static float? GetRGBValue(object value)
        {
            if (value == null) { return null; }

            try
            {
                float floatValue = float.Parse(value.ToString());
                return floatValue * 255f;
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
