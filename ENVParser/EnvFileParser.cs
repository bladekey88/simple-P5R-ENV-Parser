namespace ENVParser
{
    internal class EnvFileParser
    {
        private readonly List<DataDictionary.DataDictionaryEntry> _dataDictionary;
        private readonly FieldTypeConverter _fieldTypeConverter;

        public EnvFileParser(List<DataDictionary.DataDictionaryEntry> dataDictionary)
        {
            _dataDictionary = dataDictionary;
            _fieldTypeConverter = new FieldTypeConverter(); // Initialize the converter
        }

        public Dictionary<string, (object, string)> ExtractValues(byte[] fileBytes)
        {
            var extractedValues = new Dictionary<string, (object, string)>();
            foreach (var field in _dataDictionary)
            {
                int startIndex = field.GetHexAddressAsInt();
                int length = Convert.ToInt32(field.FieldLength);
                byte[] fieldBytes = fileBytes.Skip(startIndex).Take(length).ToArray();
                object value = _fieldTypeConverter.ConvertBytes(field.FieldType, fieldBytes);
                extractedValues[field.FieldName] = (value,field.FieldType);
            }
            return extractedValues;
        }
    }
}
