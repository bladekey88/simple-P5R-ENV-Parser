
using ENVParser;
using ENVParser.Fields;
using ENVParser.Utils;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\nUsage: ENVParser <file path> <csv|json>(optional)");
            Console.WriteLine("\n\t - Either drop an ENV or ENV.json or provide a file path as a command-line argument.");
            Console.WriteLine("\t - If proving an ENV file, you can optionally specify an output file type as the second argument.");
            Console.WriteLine("\t   JSON and CSV are supported for export (JSON is the default).");
            return;
        }

        // Check the file exists
        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"ERROR\tThe specified file does not exist: '{filePath}'");
            return;
        }

        List<string> validFileExtensions = [".ENV", ".ENV.json", ".ENV.csv"];

        if (!validFileExtensions.Any(s=>filePath.Contains(s, StringComparison.OrdinalIgnoreCase))) {
            Console.WriteLine($"ERROR\tThe file must be either an .ENV, .ENV.json, or .ENV.csv file: '{Path.GetExtension(filePath)}' provided");
            return;
        }

        // Prepare object for use.
        EnvFile envFile = new();
        EnvFile envFileHeader = new();

        // Process Files
        if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase) && Path.GetFileName(filePath).Contains(".ENV.json", StringComparison.OrdinalIgnoreCase))
        {
            try
            {

                string jsonString = File.ReadAllText(filePath);
                JsonImporter.DeserialiseJson(jsonString, envFile);

                // After deserialisation check if valid version
                ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFile.EnvHeader.GFSVersion, true);

                // Notify Users
                Console.WriteLine();
                Console.WriteLine($"INFO\tOutput ENV file will be based on the GFS Version supplied");
                Console.WriteLine($"INFO\tThe program does not validate if field presence against version");
                Console.WriteLine($"INFO\tThus extra fields that don't exist in a version will be skipped");
                Console.WriteLine($"INFO\tMissing fields that are expected by the version will be set to 0 or an empty string or equivalent");
                Console.WriteLine($"INFO\tAdditionally the 010 templates have conditional logic that may omit fields based on the version number");
                Console.WriteLine();


                // Write out to file
                string outputENV = Path.GetFullPath(filePath).ToString().Replace(".ENV.json", ".ENV");

                if (Path.Exists(outputENV))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"WARN\t'{outputENV}' already exists and will be overwritten");
                    Console.WriteLine($"WARN\tOverwriting this file CANNOT be undone");
                    Console.WriteLine($"WARN\tPress any key to proceed or ESCAPE to abort");
                    var cki = Console.ReadKey();
                    Console.ResetColor();
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine($"IINFO\tOperation aborted. The existing file has not been modified");
                        return;
                    }
                }
                using var stream = new FileStream(outputENV, FileMode.Create, FileAccess.Write);
                using BigEndianBinaryWriter writer = new(stream);
                envFile.Write(writer);

                // Update the user
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"OK\tConversion to ENV complete");
                Console.ResetColor();
                Console.WriteLine($"INFO\tENV file created at: '{outputENV}'");
                Console.WriteLine("\nPress Enter to close this window");
                Console.ReadKey(true);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"EXCEPTION\t{ex.GetType().Name}");
                Console.WriteLine($"EXCEPTION\t{ex.Message}");
                Console.ResetColor();
                Console.ReadKey(true);
            }
            return;
        }

        else if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase)) { }

        else if (Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                // Call the Read method to populate the envFileHeader instance
                long envFileSize = new FileInfo(filePath).Length;
                using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
                using BigEndianBinaryReader reader = new(fileStream);

                // Validation on GFS header
                // Use the enum from the class - though when refactoring lets pull this into its own class
                ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFileHeader.ReadHeader(reader).GFSVersion, true);

                // Validation on file size against version header
                EnvValidator.Validate(envFileHeader, envFileSize);

                // As we are using the same reader as the header, reset it to position 0
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                envFile.Read(reader);


                // Determine the output and write it
                string outputFileExtension = args.Length == 2 && args[1].Contains("csv", StringComparison.OrdinalIgnoreCase)
                    ? "csv"
                    : "json";
                string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", $".ENV.{outputFileExtension}");

                IExporter exporter = outputFileExtension == "csv" ? new CsvExporter() : new JsonExporter();
                exporter.Export(outputFile, envFile);

                // Update the user
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"OK\tConversion to {outputFileExtension.ToUpper()} complete");
                Console.ResetColor();
                Console.WriteLine($"INFO\t{outputFileExtension.ToUpper()} file created at: '{outputFile}'");
                Console.WriteLine("\nPress Enter to close this window");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"EXCEPTION\t{ex.GetType().Name}");
                Console.WriteLine($"EXCEPTION\t{ex}");
                Console.ResetColor();
            }
            return;

        }

    }
}