using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser
{
    internal class CsvWriter
    {
        public static void WriteToFile(string filePath, List<string>data) {

            string outputDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(outputDirectory) && outputDirectory != "") {
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

