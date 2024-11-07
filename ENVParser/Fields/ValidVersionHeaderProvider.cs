namespace ENVParser.Fields
{
    internal class ValidVersionHeaderProvider
    {
        public static GameVersions CheckValidVersion(uint versionNumber)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(versionNumber);

            if (_p5RoyalGfsVersions.Contains(versionNumber))
            {
                return GameVersions.P5Royal;
            }

            if (_p5VanillaGfsVersions.Contains(versionNumber) || _p5BetaGfsVersions.Contains(versionNumber))
            {
                return GameVersions.P5Vanilla;
            }

            throw new ArgumentException("The program exited due to an unsupported or invalid GFS Version");

        }
        public static GameVersions CheckValidVersion(uint versionNumber, bool showOutput)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(versionNumber);

            string hexValue = "0x" + versionNumber.ToString("X7");
            Console.WriteLine($"INFO\tSupplied GFS Version is {hexValue} ({versionNumber})");

            if (_p5RoyalGfsVersions.Contains(versionNumber))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK\tGFS Version matches P5 Royal ENV");
                Console.ResetColor();
                return GameVersions.P5Royal;
            }

            if (_p5VanillaGfsVersions.Contains(versionNumber))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK\tGFS Version matches Persona 5 (Vanilla)");

                if (_p4DancingGfsVersions.Contains(versionNumber))
                {
                    Console.WriteLine("OK\tGFS Version matches Persona 4 Dancing");
                }

                if (_p5BetaGfsVersions.Contains(versionNumber))
                {
                    Console.WriteLine("OK\tGFS Version matches Persona 5 Beta");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("WARN\tPersona 5 (Vanilla) mapping will be used.");
                    Console.WriteLine("WARN\tENVs from the Beta may not behave as expected");
                }
;
                Console.ResetColor();
                return GameVersions.P5Vanilla;
            }

            // Error out for unsupported/invalid versions
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR\tUnsupported ENV Detected");
            Console.WriteLine("REASON\tGFS Version does not match known ENV Versions for P5R, P5 Vanilla, or P5 Beta");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to close this window");
            Console.ReadKey(true);
            throw new ArgumentException("The program exited due to an unsupported or invalid GFS Version");

        }

        public enum GameVersions : sbyte
        {
            Unknown = -1,
            P5Vanilla = 0,
            P5Royal = 1,
            P4Dancing = 2,
            P5Beta = 99
        }

        private static readonly HashSet<uint> _p5RoyalGfsVersions =
        [
            // Royal only has one GFS Version, so that makes things easy
            17846608
        ];

        private static readonly HashSet<uint> _p5VanillaGfsVersions =
        [
            // Vanilla has many GFS Version, and they are shared with the beta
            // However 17846384	(0x1105070) is unique to Vanilla and also the most prevalent
            17846384,
            17846368,
            17846336,
            17846320,
            17846272,
            17846304,
            17846288,
            17844592,
            17843456,
            17843968,
            17844480,
            17844544,
            17844512,
            17842688,
            17844576,
            17844624,
            17846352,
            17844224,
            17842768,
            17843712,
            17844560,
            17844496,
        ];

        private static readonly HashSet<uint> _p5BetaGfsVersions =
        [
            17846368,
            17846336,
            17846320,
            17846304,
            17846272,
            17846288,
            17844592,
            17843456,
            17846352,
            17843968,
            17844480,
            17844544,
            17844512,
            17842688,
            17844576,
            17844624,
            17844224,
            17842768,
            17843712,
            17844560,
            17844496,
        ];

        private static readonly HashSet<uint> _p4DancingGfsVersions =
        [
            // P4D only has one GFS Version, so that makes things easy
            17846336
        ];
    }
}
