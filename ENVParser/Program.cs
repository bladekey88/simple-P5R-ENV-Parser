
using ENVParser;
using System.Reflection;
using System.Xml;
using static ENVParser.DataDictionary;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please drop an ENV file onto the executable or pass it as a cmd argument.");
            Console.WriteLine();
            return;
        }

        string filePath = args[0];

        ProcessEnvFile(filePath);
    }


    private static void ProcessEnvFile(string filePath) { 
        // Set up Data Dictionary
        string resourceName = "ENVParser.Resources.ENV_FieldHexMapping.csv";
        List<DataDictionaryEntry> dataDictionary = DataDictionary.LoadDataDictionary(resourceName);

        // Read ENV Data
        //string filePath = "C:\\Users\\aruna\\Desktop\\P5R\\Modding\\P5R_DATA\\ENV\\ENV0000_000_000.ENV";
        ParseEnvFile envData = new(filePath);
        byte[] envFile = envData.ReadFile();

        //Process the ENV data against the dictionary
        List<string> envFields = [];
        foreach (DataDictionaryEntry entry in dataDictionary)
        {
            if (!entry.FieldType.Contains("struct",StringComparison.OrdinalIgnoreCase))
            {   
                ByteArrayProcessor processor = new();
                object result = processor.ProcessByteArray(envFile, entry.HexAddress, entry.FieldLength, entry.FieldType);
                string output = $"{entry.FieldName},{result},{entry.FieldType},";
                envFields.Add(output);
                }
        }

        if (envFields.Count > 0)
        {
            string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV",".csv");
            CsvWriter.WriteToFile(outputFile, envFields);
        }
    }
}