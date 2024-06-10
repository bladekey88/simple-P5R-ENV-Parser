namespace ENVParser
{
    internal class CsvWriter
    {
        public static void WriteToFile(string filePath, List<string> data)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "An output file path must be supplied.");
            }

            string outputDirectory = Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException("Failed to get directory name from file path.");
            
            if (!Directory.Exists(outputDirectory) && !string.IsNullOrEmpty(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            using FileStream stream = new(filePath, FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(stream);
            writer.WriteLine("Field, Value, Type,");
            foreach (string line in data)
            {
                writer.WriteLine(line);
            }
        }
    }
}

