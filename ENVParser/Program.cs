
using ENVParser;
using static ENVParser.DataDictionary;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\nUsage: ENVParser <file path> <csv|json>(optional)");
            Console.WriteLine("\n\t - Please either drop an env file on the executable or provide an ENV file path as a command-line argument.");
            Console.WriteLine("\t - Optionally, provide an output file type as the second argument (json or csv).");
            return;
        }

        // Check ENV file
        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"The specified file does not exist: '{filePath}'");
            return;
        }
        if (!Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"The file must be an .ENV file: '{Path.GetExtension(filePath)}' provided");
            return;
        }

        // Parse ENV File
        byte[] envFile = File.ReadAllBytes(filePath);

        // Set up Data Dictionary            
        List<DataDictionaryEntry> entries = DataDictionary.LoadDataDictionary();

        // Process the ENV data against the dictionary
        var parser = new EnvFileParser(entries);
        Dictionary<string, (object,string)> extractedValues = parser.ExtractValues(envFile);

        if (args.Length == 2)
        {
            string outputFileType = args[1];

            // For CSV Checking
            if (outputFileType.Equals("csv", StringComparison.OrdinalIgnoreCase))
            {
                string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".csv");
                CsvExporter exporter = new();
                exporter.Export(outputFile, extractedValues);
                Console.WriteLine($"CSV file created at: '{outputFile}'");
            }
        }
        return;
    }
}