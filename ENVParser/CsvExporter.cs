using CsvHelper;
using ENVParser.Fields;
using System.Globalization;

namespace ENVParser
{
    internal class CsvExporter : IExporter
    {
        private readonly HashSet<string> _validFieldsForRGBValues;

        public CsvExporter()
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

            var csvData = extractedData.Select(data => new CsvOutput
            {
                FieldName = data.Key,
                Value = data.Value.Item1?.ToString(),
                RGBValue = _validFieldsForRGBValues.Contains(data.Key) ? GetRGBValue(data.Value.Item1) : null,
                FieldType = data.Value.Item2
            }).ToList();

            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(csvData);
        }

        // Defines the structure of CSV records
        private class CsvOutput
        {
            public string FieldName { get; set; }
            public string Value { get; set; }
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

