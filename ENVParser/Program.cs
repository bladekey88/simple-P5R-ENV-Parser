
using ENVParser;
using ENVParser.Utils;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\nUsage: ENVParser <file path> <csv|json>(optional)");
            Console.WriteLine("\n\t - Either drop an ENV or provide a file path as a command-line argument.");
            //Console.WriteLine("\t - If proving an ENV file, you can optionally specify an output file type as the second argument.");
            //Console.WriteLine("\t   JSON and CSV are supported for export (JSON is the default).");
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

        EnvFile envFile = new();
        
        // Call the Read method to populate the envFile instance
        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
        using BigEndianBinaryReader reader = new(fileStream);        
        envFile.Read(reader);
        
        // Write to JSON
        string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.json");
        JsonExporter exporter = new();
        exporter.Export(outputFile, envFile);
        Console.WriteLine($"\n\tJSON file created at: '{outputFile}'");
        return;

        //using var stream = new FileStream($"{filePath}.bin", FileMode.Create, FileAccess.Write);
        //using var writer = new BigEndianBinaryWriter(stream);
        //envFile2.Write(writer);



    }
}