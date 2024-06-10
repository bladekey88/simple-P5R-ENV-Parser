namespace ENVParser
{
    internal class ParseEnvFile
    {
        private readonly string filePath;

        public ParseEnvFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The filepath cannot be null or empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified file does not exist", nameof(filePath));
            }

            if (!Path.GetExtension(filePath).Equals(".ENV", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentOutOfRangeException(nameof(filePath), "The file must be a .ENV file");
            }

            this.filePath = filePath;
            Console.WriteLine($"ENV File located at: '{filePath}'");
        }

        public byte[] ReadFile()
        {
            using FileStream fs = new(this.filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
