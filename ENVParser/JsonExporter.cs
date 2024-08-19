using ENVParser.Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ENVParser
{
    internal class JsonExporter : IExporter
    {

        private readonly HashSet<string> _validFieldsForRGBValues;

        public JsonExporter()
        {
            _validFieldsForRGBValues = RGBFieldNameProvider.GetValidFields();
        }

        public void Export(string filePath, Dictionary<string, (object, string, int, int)> extractedData)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "An output file path must be supplied.");
            }
            string outputDirectory = Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Failed to get directory name from file path.");

            // Set up sections for json from structs
            var sections = new List<Section>();
            var sectionStack = new Stack<Section>();

            foreach (var data in extractedData)
            {
                // unpack tuple and save into appropriate fields 
                var (fieldValue, fieldType, start, length) = data.Value;

                // Use a stack since this maintains add order, if a struct is encountered add it to stack, remove it from stack when field is greater than length
                if (fieldType.StartsWith("struct", StringComparison.OrdinalIgnoreCase))
                {
                    var newSection = new Section { Name = data.Key, Length = length, StartIndex = start };

                    if (sectionStack.Count > 0)
                    {
                        sectionStack.Peek().Children.Add(newSection);
                    }
                    else
                    {
                        sections.Add(newSection);
                    }

                    sectionStack.Push(newSection);
                }
                else
                {
                    if (sectionStack.Count == 0)
                    {
                        throw new InvalidOperationException("Field without a preceding section");
                    }

                    sectionStack.Peek().Fields.Add(new JsonOutput
                    {
                        FieldName = data.Key,
                        FieldValue = fieldValue != null ? JToken.FromObject(fieldValue) : null,
                        RGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(fieldValue) : null,
                        FieldType = fieldType
                    });
                }

                // Removal of struct from stack
                if (!fieldType.StartsWith("struct", StringComparison.OrdinalIgnoreCase))
                {
                    while ((sectionStack.Count > 0) && ((start + length) >= sectionStack.Peek().StartIndex + sectionStack.Peek().Length))
                    {
                        sectionStack.Pop();
                    }
                }
            }

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string json = JsonConvert.SerializeObject(sections, Formatting.Indented, settings);
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

        private class Section
        {
            public string Name { get; set; }
            public int Length { get; set; }
            public int StartIndex { get; set; }
            public List<Section> Children { get; set; } = [];
            public List<JsonOutput> Fields { get; set; } = [];
            public Section Parent { get; set; }

        }
    }
}
