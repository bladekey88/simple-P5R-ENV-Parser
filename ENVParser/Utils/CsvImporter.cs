using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ENVParser.Utils
{
    internal class CsvImporter
    {
        public CsvImporter() {}

        public static void ProcessCsv(string filePath, EnvFile envFile)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var csvRecords = csv.GetRecords<Field>();
            Dictionary<string, object> csvData = [];
            IterateThroughCsv(csvRecords, csvData);
            envFile.Add(csvData);
        }

        private static void IterateThroughCsv(IEnumerable<Field> csvRecord, Dictionary<string, object> csvData)
        {
            string? currentSection = null;
            Dictionary<string, object> sectionData = [];

            foreach (var row in csvRecord)
            {
                row.ConvertFieldValue();

                if (row.ActualValue is string)
                {
                    // If a new section is encountered, store the previous section's data
                    if (currentSection != null)
                    {
                        csvData.Add(currentSection, sectionData);
                    }
                    
                    currentSection = row.FieldName.Replace(" ","");
                    sectionData = [];
                }
                else
                {
                    sectionData.Add(row.FieldName, row.ActualValue);
                }
            }

            // Add the last section's data to the main dictionary
            if (currentSection != null)
            {
                csvData.Add(currentSection, sectionData);
            }
        }

        private class Field
        {
            public string FieldName { get; set; }
            public string Value { get; set; }
            public float? RGBValue { get; set; }
            public string FieldType { get; set; }
            public string? Comments { get; set; }
            
            // As the text is a string we need a field to handle the real values
            public Object? ActualValue { get; set; } 

            public void ConvertFieldValue()
            {
                try
                {
                   
                    switch (FieldType)
                    {
                        case "Boolean":
                            ActualValue = Convert.ToBoolean(Value);
                            break;
                        case "Unsigned 32-bit Integer (u32)":
                            ActualValue = Convert.ToUInt32(Value);
                            break;
                        case "Unsigned 8-bit Integer (u8)":
                            ActualValue = Convert.ToByte(Value);
                            break;
                        case "32-bit floating point (f32)":
                            ActualValue = Convert.ToDouble(Value);
                            break;                            
                        case "Section":
                            ActualValue = "";
                            break;
                        default:
                            throw new InvalidDataException();
                    }
                    
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex);
                    Console.WriteLine($"INVALID {this.FieldName} - {this.Value} - {this.FieldType}");
                }
            }
        }
    }
}
