using System.Reflection;

namespace ENVParser
{
    public class DataDictionary
    {

        public class DataDictionaryEntry(string fieldName, int hexAddress, int fieldLength, string fieldType)
        {
            public string FieldName { get; set; } = fieldName ?? throw new ArgumentNullException(nameof(fieldName));
            public int HexAddress { get; set; } = hexAddress;
            public int FieldLength { get; set; } = fieldLength;
            public string FieldType { get; set; } = fieldType ?? throw new ArgumentNullException(nameof(fieldType));

            public override string ToString()
            {
                return $"{FieldName} ({FieldType}), Address: {HexAddress}, Length: {FieldLength}";
            }
        }
        public static List<DataDictionaryEntry> LoadDataDictionary(string resourceName)
        {
            var entries = new List<DataDictionaryEntry>();
            var assembly = Assembly.GetExecutingAssembly();

            // Load resource
            Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new Exception($"Resource {resourceName} not found.");
            try
            {
                using StreamReader reader = new(stream);
                string? line = reader.ReadLine();
                while ((line = reader.ReadLine()) is not null)
                {
                    var parts = line.Split(',');
                    string fieldName = parts[0];
                    var hexAddressAll = parts[1].Split("h");
                    int hexAddress = Convert.ToInt32(hexAddressAll[0], 16);
                    int fieldLength = int.Parse(parts[2]);
                    string fieldType = parts[3];
                    var entry = new DataDictionaryEntry(fieldName, hexAddress, fieldLength, fieldType);
                    entries.Add(entry);
                }
            }
            finally { stream.Dispose(); }

            return entries;
        }
    }
}
