using System.Text.Json;

namespace ENVParser
{
    internal class JsonImporter
    {
        public JsonImporter() { }

        public static List<Field> DeserialiseJson(string json)
        {
            // Deserialise to a list of field Objects, which can
            // then be mapped onto the ENV Object back in main.
            // This was to prevent the IEnumerable interface throwing an error
            // when trying to deseriable directly to EnvFile. 
            List<Field> deserialisedJson = JsonSerializer.Deserialize<List<Field>>(json);

            foreach (Field field in deserialisedJson)
            {
                field.ConvertFieldValue();
            }
            return deserialisedJson;

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
                                FieldValue = jsonElement.GetUInt32();
                                break;
                            case "f32":
                                FieldValue = jsonElement.GetSingle();

                                break;
                            case "boolean":
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

                catch (InvalidDataException ex)
                {
                    Console.WriteLine($"Processing Error: {ex.Message}");
                    Console.WriteLine("\t - Reason: JSON has unexpected 'FieldType': ");
                    Console.WriteLine($"\t - Data: FieldName: '{FieldName}' FieldType: '{FieldType}'");
                }
            }
        }

    }
}