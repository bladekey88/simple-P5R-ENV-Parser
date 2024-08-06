
using ENVParser;
using static ENVParser.DataDictionary;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please either drop an env file on the executable or provide an ENV file path as a command-line argument.");
            Console.WriteLine("Optionally, provide an output file type as the second argument (json or csv).");
            Console.WriteLine();
            return;
        }

        // Assign command line arguments as necessary and then pass to method
        string filePath = args[0];

        if (args.Length == 2)
        {
            string outputFileType = args[1];
            ProcessEnvFile(filePath, outputFileType);
        }
        else
        {
            ProcessEnvFile(filePath);
        }
        return;

    }


    private static void ProcessEnvFile(string filePath, string outputFileType = "json")
    {

        // Set up Data Dictionary
        string resourceName = "ENVParser.Resources.ENV_FieldHexMapping.csv";
        List<DataDictionaryEntry> dataDictionary = DataDictionary.LoadDataDictionary(resourceName);

        // Read ENV Data
        ParseEnvFile envData = new(filePath);
        byte[] envFile = envData.ReadFile();

        //Process the ENV data against the dictionary
        List<string> envFields = [];
        foreach (DataDictionaryEntry entry in dataDictionary)
        {
            if (!entry.FieldType.Contains("struct", StringComparison.OrdinalIgnoreCase))
            {
                ByteArrayProcessor processor = new();
                object result = processor.ProcessByteArray(envFile, entry.HexAddress, entry.FieldLength, entry.FieldType);
                string output = $"{entry.FieldName},{result},{entry.FieldType},";
                envFields.Add(output);
            }
        }

        // Write the file out
        if (envFields.Count > 0)
            if (outputFileType.Equals("csv", StringComparison.OrdinalIgnoreCase))
            {
                {
                    string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".csv");
                    CsvWriter.WriteToFile(outputFile, envFields);
                    Console.WriteLine($"CSV file created at: '{outputFile}'");
                }
            }
            else { Console.WriteLine("JSON Functionality has not been implemented"); }
    }
}