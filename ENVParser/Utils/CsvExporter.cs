using CsvHelper;
using ENVParser.Fields;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENVParser.Utils
{
    internal class CsvExporter : IExporter
    {
        private readonly HashSet<string> _validFieldsForRGBValues;

        // Defines the structure of CSV records
        private class CsvOutput
        {
            public string FieldName { get; set; }
            public string Value { get; set; }
            public float? RGBValue { get; set; }
            public string FieldType { get; set; }
            public string? Comments { get; set; }
        }

        public CsvExporter()
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR\tAn error occurred");
            Console.WriteLine("REASON\tCSV EXPORT DISABLED PENDING RE-WRITE. USE AN OLDER VERSION");
            Console.ResetColor();
            throw new NotImplementedException();
            // Derive GameVersion
            //ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFile.GFSVersion);

            // Get valid fields for GFS Version
            //List<string> validFields = P5VersionsFieldsProvider.GetP5UniqueVersionFields(envFile.GFSVersion);



            // Derive GameVersion
            ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFile.GFSVersion);

            // Get valid fields for GFS Version
            List<string> validFields = P5VersionsFieldsProvider.GetP5UniqueVersionFields(envFile.GFSVersion);

            // Create output dictionary
            List<CsvOutput> csvData = [];

            foreach (var data in envFile)
            {
                if (!validFields.Contains(data.Key)) { continue; }
                var fieldValue = data.Value;
                var fieldRGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(data.Value) : null;
                var fieldType= TextReadableFieldType(data.Value.GetType().Name);

                CsvOutput innerList = new()
                {
                    FieldName = data.Key,
                    Value = data.Value.ToString(),
                    RGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(data.Value) : null,
                    FieldType = TextReadableFieldType(data.Value.GetType().Name),
                };
                csvData.Add(innerList);
            }

            // Obsolete Code - remove when ready
            //As this is a one - way transfer
            //No need for special override for Texture Section so, can override value to 0
            // However also need to do a game version check to remove unneeded fields
            //var csvData = envFile.Where(data =>
            //{
            //    // This statement is used to not select the fields missing from the HashSet if we don't use P5R
            //    // thus not passing them to the CSV generator below
            //    return (validFields.Contains(data.Key));
            //}).Select(data => new CsvOutput
            //{
            //    FieldName = data.Key,
            //    Value = data.Value.ToString(),
            //    RGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(data.Value) : null,
            //    FieldType = TextReadableFieldType(data.Value.GetType().Name),
            //}
            //).ToList();

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
                case "String":
                    readableFieldType = "";
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
