
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

        // Only accept ENV or ENV.json files (for now)
        if (!Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase) && !Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"ERROR\tThe file must be an .ENV or an .ENV.json file: '{Path.GetExtension(filePath)}' provided");
            return;
        }

        // Prepare object for use.
        EnvFile envFile = [];
        EnvFile envFileHeader = [];

        // Process Files
        if (Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var deserialisedJson = JsonImporter.DeserialiseJson(jsonString);

                // Update Env Object with data - special override needed for reserve
                // ByteArray should max out at 188.
                byte[] reserveSection = new byte[188];
                int byteIndex = 0;

                foreach (var item in deserialisedJson)
                {
                    if (item.FieldName.Contains("Reserve", StringComparison.OrdinalIgnoreCase))
                    {
                        reserveSection[byteIndex] = (byte)item.FieldValue;

                        // Override Name and Value for the reserve fields
                        if (byteIndex < 187)
                        {
                            byteIndex++;
                            continue;
                        }
                        else
                        {
                            item.FieldName = "UnusedTextureSection";
                            item.FieldValue = reserveSection;
                        }
                    }
                    envFile.Add(item.FieldName, item.FieldValue);
                }

                // Write out to file
                string outputENV = Path.GetFullPath(filePath).ToString().Replace(".ENV.json", ".ENV");
                using var stream = new FileStream(outputENV, FileMode.Create, FileAccess.Write);
                using BigEndianBinaryWriter writer = new(stream);
                envFile.Write(writer);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
        else if (Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                // Call the Read method to populate the envFile instance
                using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
                using BigEndianBinaryReader reader = new(fileStream);
                envFileHeader.ReadHeader(reader);

                // Validation on GFS header
                // Use the enum from the class - though when refactoring lets pull this into its own class
                ValidVersionHeaderProvider.GameVersions gameVersion = ValidVersionHeaderProvider.CheckValidVersion(envFileHeader.GFSVersion);
                
                if (!gameVersion.Equals(ValidVersionHeaderProvider.GameVersions.P5Royal))
                {   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR\tCurrently only P5 Royal ENVs are supported ({gameVersion} supplied)");
                    Console.ResetColor();
                    Console.WriteLine("\n\nPress any key to close this window");
                    Console.ReadKey(true);
                    throw new NotImplementedException($"The program exited due to an unsupported game version");
                }

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
                Console.WriteLine($"OK\tConversion to {outputFileExtension} complete");
                Console.ResetColor();
                Console.WriteLine($"INFO\tJSON file created at: '{outputFile}'");
                Console.WriteLine("\nPress Enter to close this window");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"EXCEPTION\t{ex.GetType().Name}");
                Console.WriteLine($"EXCEPTION\t{ex.Message}");
                Console.ResetColor();
            }
            return;

        }

    }
}