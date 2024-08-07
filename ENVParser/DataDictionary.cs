using CsvHelper;
using System.Globalization;
using System.Reflection;

namespace ENVParser
{
    internal class DataDictionary
    {
        private const string DD_FILE_PATH = "ENVParser.Resources.ENV_FieldHexMapping.csv";
        public class DataDictionaryEntry()
        {
            public string FieldName { get; set; }
            public string HexAddress { get; set; }
            public int FieldLength { get; set; }
            public string FieldType { get; set; }

            public override string ToString()
            {
                return $"{FieldName} ({FieldType}), Address: {HexAddress}, Length: {FieldLength}";
            }

            public int GetHexAddressAsInt()
            {
                return int.Parse(HexAddress.TrimEnd('h'), System.Globalization.NumberStyles.HexNumber);
            }
        }
        public static List<DataDictionaryEntry> LoadDataDictionary()
        {
            // Get embedded resource containing Data Dictionary CSV
            var entries = new List<DataDictionaryEntry>();
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(DD_FILE_PATH) ?? throw new Exception($"Resource {DD_FILE_PATH} not found.");

            // Create a reader, then parse with csvHElper
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<DataDictionaryEntry>();

            // Add to entries list and return
            entries.AddRange(records);
            if (entries.Count == 0)
            {
                throw new InvalidProgramException("Data Dictionary source file is empty. Please report an issue on github.");
            }
            return entries;
        }
    }
}
