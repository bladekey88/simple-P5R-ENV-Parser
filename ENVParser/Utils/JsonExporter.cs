using ENVParser.Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ENVParser.Utils
{
    internal class JsonExporter : IExporter
    {

        private readonly HashSet<string> _validFieldsForRGBValues;

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

            if (Path.GetDirectoryName(filePath) is null)
            {
                throw new InvalidOperationException("Failed to get directory name from file path.");
            }

            // Get valid fields for GFS Version
            List<string> validFields = P5VersionsFieldsProvider.GetP5UniqueVersionFields(envFile.GFSVersion);

            // Dictionary to hold final values for serialisation
            Dictionary<string, Object> output = [];

            // Use reflection to get the values out
            foreach (PropertyInfo pi in envFile.GetType().GetProperties())
            {
                object propertyValue = pi.GetValue(envFile);
                if (propertyValue != null)
                {
                    if (propertyValue.GetType().IsClass)
                    {
                        List<object> innerDict = [];
                        // Recursively iterate over nested objects
                        foreach (PropertyInfo nestedPropertyInfo in propertyValue.GetType().GetProperties())
                        {
                            var fieldType = "f32";  // Sets up variable

                            // Skip fields based on version number
                            if (!validFields.Contains(nestedPropertyInfo.Name))
                            {
                                continue;
                            }

                            object nestedPropertyValue = nestedPropertyInfo.GetValue(propertyValue);
                            switch (nestedPropertyValue)
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

                            JsonOutput jsonOutput = new()
                            {
                                FieldName = nestedPropertyInfo.Name,
                                FieldValue = nestedPropertyValue != null ? JToken.FromObject(nestedPropertyValue) : null,
                                RGBValue = _validFieldsForRGBValues.Contains(nestedPropertyInfo.Name) ? GetRGBValue(nestedPropertyValue) : null,
                                FieldType = fieldType,
                                Comment = pi.Name.Contains("FieldModelLight1") || pi.Name.Contains("FieldModelLight2") ? "SECTION UNUSED IN P5/P5B/P5R" : null
                            };

                            innerDict.Add(jsonOutput);
                        }
                        output.Add(pi.Name, innerDict);
                    }
                }
            }
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string json = JsonConvert.SerializeObject(output, Formatting.Indented, settings);
            using StreamWriter sw = new(filePath);
            {
                sw.Write(json);
            }

            return;
        }

        // Defines the structure of JSON records
        private class JsonOutput
        {
            public string FieldName { get; set; }
            public JToken? FieldValue { get; set; }
            public float? RGBValue { get; set; }
            public string FieldType { get; set; }
            public string? Comment { get; set; }
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
