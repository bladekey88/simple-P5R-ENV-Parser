using CsvHelper;
using System.Collections;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text.Json;
using static ENVParser.JsonImporter;

namespace ENVParser
{
    internal class JsonImporter
    {
        public JsonImporter() { }

        public static List<Field> DeserialiseJson(string json)
        {
            List<JsonNode> deserialisedJson = JsonSerializer.Deserialize<List<JsonNode>>(json);

            // Only care about the "fields" parts of the json (not the structural parts), so flatten the json using LINQ
            var allFields = deserialisedJson.SelectMany(envStruct =>
            {
                // Flatten fields from the direct attached to structural nodes
                // This is the 4 header field, and the boolean toggle fields in the param section and the footer
                var fieldsFromEnv = envStruct.Fields;

                // Flatten fields from child and grandchild nodes of ENV Param section (no need to go deeper)
                // These are the actual data fields - do this due to Field Model Section sub struct
                var fieldFromEnvParamSection = envStruct.Children.SelectMany(child =>
                {
                    var fieldsFromChild = child.Fields;
                    var fieldsFromGrandchildren = child.Children.SelectMany(grandchild => grandchild.Fields);
                    return fieldsFromChild.Concat(fieldsFromGrandchildren);
                });

                return fieldsFromEnv.Concat(fieldFromEnvParamSection);
            }, (envStruct, fields) => fields) // Flatten the result
            .ToList();

            foreach (Field field in allFields) {
                field.ConvertFieldValue();    
            }

            /* Old Flattening implementation (messy and overly verbose)            
            // Parse Field and convert to system types
            foreach (var envStruct in deserialisedJson)
            {
                // Parse fields at the top level, which are connected to the root structs(ENV Header and ENV Param)
                // This parses the four header fields, and the two boolean flag fields in the param section
                foreach (var field in envStruct.Fields) { field.ConvertFieldValue(); }

                // Then parse the children of the roots - this only affects ENV Param
                // Technically the Field Model section has a nested field, but for now this will be ignored
                foreach (var envParamSection in envStruct.Children)
                {
                    // Actual ENV data in the Children Fields
                    foreach (Field childField in envParamSection.Fields) { childField.ConvertFieldValue(); }
                    
                    // Special Check for Reserve[47], which is nested in the Field Model Section, therefore need to check its children
                    foreach (var nestedChild in envParamSection.Children)
                    {
                        foreach (var nestedField in nestedChild.Fields) { nestedField.ConvertFieldValue(); }                        
                    }
                }
            } */

            return allFields;
        }

        public static void WriteJsonToENV(string filePath, List<Field> deserialisedJsonFields, List<DataDictionary.DataDictionaryEntry> entries)
        {
            // TODO - need to refactor to maintain correct order. 
            // TODO - create an ENV class that has all of the ENV data in one place for ease of use
            
            // Check if all required fields are present (by key FieldName)
            foreach (var field in entries)
            {
                if (!deserialisedJsonFields.Any(f => f.FieldName == field.FieldName) && (!field.FieldType.Contains("struct",StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidDataException($"Error - Field '{field.FieldName}' is missing from the JSON.");
                }
            }

            // Check for duplicate fields in the input list
            List<string> duplicateFields = deserialisedJsonFields.GroupBy(f => f.FieldName)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            
            if (duplicateFields.Any())
            {
                throw new InvalidDataException($"Error - Duplicate fields found in the input list: {string.Join(", ",duplicateFields)}");
            }

            // Check for any extra fields which should not be considered
            var extraFields = deserialisedJsonFields.Where(f => !entries.Any(e => e.FieldName == f.FieldName)).ToList();
            if (extraFields.Any())
            {
                throw new InvalidDataException($"Error - The following fields are part of the JSON but are not part of the ENV Definition: {string.Join(", ", extraFields.Select(f => f.FieldName))}");
            }

            Queue outputFields = new();
            // Write to File
            foreach (Field field in deserialisedJsonFields) {
                var fieldValue = field.FieldValue;
                string fieldType = field.FieldType;                   
                outputFields.Enqueue(FieldTypeConverter.ConvertToBytes(fieldType,fieldValue));
            }

            using var fileStream = new FileStream(filePath, FileMode.Create);
            using var binaryWriter = new BinaryWriter(fileStream);
            {
                foreach (var row in outputFields)
                {
                    if (row is byte b) { binaryWriter.Write(b); }
                    else if (row is byte[] bytes) { binaryWriter.Write(bytes); }
                    else { throw new InvalidOperationException($"Unexpected type: {row.GetType()}"); }
                }
            }

        }




        public class JsonNode
        {
            public List<JsonNode> Children { get; set; } = [];
            public List<Field>? Fields { get; set; } = [];
        }

        public class Field
        {
            public required string FieldName { get; set; }
            public required Object FieldValue { get; set; }
            public required string FieldType { get; set; }

            public void ConvertFieldValue()
            {
                try
                {
                    if (FieldValue is JsonElement jsonElement)
                    {
                        switch (FieldType)
                        {
                            case "u32":
                                FieldValue = jsonElement.GetInt32();
                                break;
                            case "f32":
                                FieldValue = jsonElement.GetSingle();

                                break;
                            case "enum Boolean":
                                FieldValue = jsonElement.GetBoolean();

                                break;
                            case "u8":
                                FieldValue = jsonElement.GetByte();
                                break;
                            default:
                                throw new InvalidDataException();
                        }
                    }
                }
                catch (JsonException ex)
                {
                    // Handle JSON parsing errors
                    Console.WriteLine($"Error parsing JSON: {ex.Message}");
                    throw;
                }
                catch (InvalidOperationException ex)
                {
                    // Handle conversion errors
                    Console.WriteLine($"Error converting value: {ex.Message}");
                    throw;
                }

                catch (InvalidDataException ex) {
                    Console.WriteLine($"Processing Error: {ex.Message}");
                    Console.WriteLine("\t - Reason: JSON has unexpected 'FieldType': ");
                    Console.WriteLine($"\t - Data: FieldName: '{FieldName}' FieldType: '{FieldType}'");
                }
            }
        }

    }
}
