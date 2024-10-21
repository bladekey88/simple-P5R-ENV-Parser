
using ENVParser;
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
            Console.WriteLine($"The specified file does not exist: '{filePath}'");
            return;
        }

        // Only accept ENV or ENV.json files (for now)
        if (!Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase) && !Path.GetExtension(filePath).Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"The file must be an .ENV or an .ENV.json file: '{Path.GetExtension(filePath)}' provided");
            return;
        }

        // Prepare object for use.
        EnvFile envFile = [];

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
                envFile.Read(reader);

                // For now exit if an unsupported GFS Version is detected
                if (envFile.GFSVersion != 17846608)
                {
                    string hexValue = "0x" + envFile.GFSVersion.ToString("X7");                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Error:\tUnsupported ENV Detected");
                    Console.WriteLine("Reason:\tExpected GFS Version is 0x1105150 (17846608)");
                    Console.WriteLine($"Reason:\tSupplied GFS Version is {hexValue} ({envFile.GFSVersion})");
                    Console.ResetColor();
                    Console.WriteLine("Press enter to close this window");
                    Console.ReadKey();
                    throw new Exception("Unsupported ENV Version");
                }

                if (args.Length == 2 && args[1].Contains("csv", StringComparison.CurrentCultureIgnoreCase))
                { //Write to CSV
                    string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.csv");
                    CsvExporter exporter = new();
                    exporter.Export(outputFile, envFile);
                    Console.WriteLine($"\n\tCSV file created at: '{outputFile}'");
                }
                else
                {
                    // Write to JSON
                    string outputFile = Path.GetFullPath(filePath).ToString().Replace(".ENV", ".ENV.json");
                    JsonExporter exporter = new();
                    exporter.Export(outputFile, envFile);
                    Console.WriteLine($"\n\tJSON file created at: '{outputFile}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return;

        }

    }
}