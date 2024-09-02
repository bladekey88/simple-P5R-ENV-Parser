
using ENVParser;
using ENVParser.Utils;
using static ENVParser.DataDictionary;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\nUsage: ENVParser <file path> <csv|json>(optional)");
            Console.WriteLine("\n\t - Either drop an ENV or ENV.json file on the executable, or provide a file path as a command-line argument.");
            Console.WriteLine("\t - If proving an ENV file, you can optionally specify an output file type as the second argument.");
            Console.WriteLine("\t   JSON and CSV are supported for export (JSON is the default).");
            return;
        }

        // Check the file exists
        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"The specified file does not exist: '{filePath}'");
            return;
        }

        // Only accept ENV or ENV.json files
        if (!Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase) && !Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"The file must be an .ENV or an .ENV.json file: '{Path.GetExtension(filePath)}' provided");
            return;
        }

        // New Version - Call the EnvFile Class to read ENV file
        EnvFile envFile2 = new();
        using (FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read))
        using (BigEndianBinaryReader reader = new(fileStream))
        {
            // Call the Read method to populate the envFile instance
            envFile2.Read(reader);
        }
        Console.WriteLine($"EnableFieldModelSection: {envFile2.EnableFieldModelSection}");

        using var stream = new FileStream($"{filePath}.bin", FileMode.Create, FileAccess.Write);
        using var writer = new BigEndianBinaryWriter(stream);
        envFile2.Write(writer);


        return;

        // Set up Data Dictionary            
        List<DataDictionaryEntry> dataDictionaryEntries = DataDictionary.LoadDataDictionary();
        var parser = new EnvFileParser(dataDictionaryEntries);

        // Handle JSON first
        if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var fields = JsonImporter.DeserialiseJson(jsonString);
                string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV.json", ".ENV");
                JsonImporter.WriteJsonToENV(outputFile, fields, dataDictionaryEntries);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }

        // else parse ENV File
        byte[] envFile = File.ReadAllBytes(filePath);

        // Process the ENV data against the dictionary        
        Dictionary<string, (object, string, int, int)> extractedValues = parser.ExtractValues(envFile);

        if (args.Length == 1)
        {
            string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.json");
            JsonExporter exporter = new();
            exporter.Export(outputFile, extractedValues);
            Console.WriteLine($"\n\tJSON file created at: '{outputFile}'");
        }
        if (args.Length == 2)
        {
            string outputFileType = args[1];

            // For CSV Checking
            if (outputFileType.Equals("csv", StringComparison.OrdinalIgnoreCase))
            {
                string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.csv");
                CsvExporter exporter = new();
                exporter.Export(outputFile, extractedValues);
                Console.WriteLine($"\n\tCSV file created at: '{outputFile}'");
            }
            else if (outputFileType.Equals("json", StringComparison.OrdinalIgnoreCase))
            {
                string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.json");
                JsonExporter exporter = new();
                exporter.Export(outputFile, extractedValues);
                Console.WriteLine($"\n\tJSON file created at: '{outputFile}'");
            }
        }
        return;
    }
}