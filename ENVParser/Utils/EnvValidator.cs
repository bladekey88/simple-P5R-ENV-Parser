namespace ENVParser.Utils
{
    internal class EnvValidator
    {
        public static void Validate(EnvFile envFile, long fileSize)
        {
            if (_ENVHeaderVersionSize.TryGetValue(envFile.GFSVersion, out uint expectedENVFileSize))
            {
                Console.WriteLine($"INFO\tSupplied File Size Version is {fileSize} bytes");
                if (fileSize != expectedENVFileSize)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR\tENV File Size/Version Mismatch");
                    Console.WriteLine("REASON\tGFS Version does not match expected file size for ENV");
                    Console.WriteLine($"REASON\tExpected File Size is {expectedENVFileSize} bytes");
                    Console.ResetColor();
                    //Console.WriteLine("\nPress any key to close this window");
                    //Console.ReadKey(true);
                    throw new ArgumentException("The program exited due to a file size/version mismatch");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("OK\tFile Size and GFS Version match");
                    Console.ResetColor();
                }

                return;
            }
        }

        private static readonly Dictionary<uint, uint> _ENVHeaderVersionSize = new()
        {
            {17846608, 825 },
            {17846384, 693 },
            {17846368, 693 },
            {17846352, 693 },
            {17846336, 680 },
            {17846320, 680 },
            {17846304, 672 },
            {17846288, 672 },
            {17846272, 668 },
            {17844624, 668 },
            {17844592, 668 },
            {17844576, 664 },
            {17844560, 664 },
            {17844544, 664 },
            {17844512, 664 },
            {17844496, 664 },
            {17844480, 664 },
            {17844224, 664 },
            {17843968, 664 },
            {17843712, 660 },
            {17843456, 660 },
            {17842768, 644 },
            {17842688, 550 },
        };
    }
}
