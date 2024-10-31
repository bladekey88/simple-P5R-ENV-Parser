using CsvHelper;
using ENVParser.Fields;
using System.Globalization;

namespace ENVParser.Utils
{
    internal class CsvExporter:IExporter
    {
        private readonly HashSet<string> _validFieldsForRGBValues;
        private readonly HashSet<string> _P5RUniqueFields;

        // Defines the structure of CSV records
        private class CsvOutput
        {
            public string FieldName { get; set; }
            public string Value { get; set; }
            public float? RGBValue { get; set; }
            public string FieldType { get; set; }
        }

        public CsvExporter()
        {
            _validFieldsForRGBValues = RGBFieldNameProvider.GetValidFields();
            _P5RUniqueFields = P5ROnlyFieldsProvider.GetP5RUniqueFields();
        }

        public void Export(string filePath, EnvFile envFile)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "An output file path must be supplied.");
            }
            string outputDirectory = Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Failed to get directory name from file path.");
            
            // Derive GameVersion
            ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFile.GFSVersion);

            // As this is a one-way transfer
            // No need for special override for Texture Section so, can override value to 0
            // However also need to do a game version check to remove unneeded fields
            var csvData = envFile.Where(data =>
            {
                // This statement does not selects the fields not in the HashSet if we don't use P5R
                // thus not passing them to the CSV generator below
                return !(_P5RUniqueFields.Contains(data.Key) && !gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal));
            }).Select(data => new CsvOutput
            {
                FieldName = data.Key,
                Value = !data.Key.Equals("UnusedTextureSection", StringComparison.Ordinal) ? data.Value.ToString() : "0",
                RGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(data.Value) : null,
                FieldType = TextReadableFieldType(data.Value.GetType().Name),
            }
            ).ToList();

            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(csvData);
        }

        private static string TextReadableFieldType(string fieldType)
        {

            string readableFieldType;
            switch (fieldType)
            {
                case "Boolean":
                    readableFieldType = "Boolean";
                    break;
                case "UInt32":
                    readableFieldType = "Unsigned 32-bit Integer (u32)";
                    break;
                case "Byte":
                    readableFieldType = "Unsigned 8-bit Integer (u8)";
                    break;
                case "Single":
                    readableFieldType = "32-bit floating point (f32)";
                    break;
                case "Byte[]":
                    readableFieldType = "Byte Array";
                    break;

                default:
                    Console.WriteLine($"Unhandled data type: {fieldType}");
                    readableFieldType = fieldType;
                    break;
            }
            return readableFieldType;
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
