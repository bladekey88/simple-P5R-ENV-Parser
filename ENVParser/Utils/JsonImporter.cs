using System.Text.Json;

namespace ENVParser.Utils
{
    internal class JsonImporter
    {
        public JsonImporter() { }
        public static Root DeserialiseJson(string json, EnvFile envFile)
        {
            Root rootObject = JsonSerializer.Deserialize<Root>(json);

            // Update the envFile
            Dictionary<string, object> jsonData = [];
            TraverseRoot(rootObject, jsonData);
            envFile.Add(jsonData);

            return rootObject;
        }

        public static void TraverseRoot(object obj, Dictionary<string, object> result, string? propertyName = null)
        {
            // If we in a parent node (i.e. under EnvHeader)
            if (obj is List<Field> fieldList)
            {
                Dictionary<string, object> tempDict = [];
                foreach (Field field in fieldList)
                {
                    field.ConvertFieldValue();
                    tempDict.Add(field.FieldName, field.FieldValue);
                }
                result.Add(propertyName, tempDict);
            }
            // Otherwise work recursively
            else if (obj != null)
            {
                var properties = obj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(obj);
                    TraverseRoot(propertyValue, result, property.Name);
                }
            }
        }

        public class Root
        {
            public List<Field> EnvHeader { get; set; }
            public List<Field> FieldModelLight0 { get; set; }
            public List<Field> FieldModelLight1 { get; set; }
            public List<Field> FieldModelLight2 { get; set; }
            public List<Field> CharacterModelLight { get; set; }
            public List<Field> Fog { get; set; }
            public List<Field> GlobalLightingEffects { get; set; }
            public List<Field> UnknownEffects { get; set; }
            public List<Field> FieldShadows { get; set; }
            public List<Field> FieldShadowColours { get; set; }
            public List<Field> ColourCorrections { get; set; }
            public List<Field> SecondUnknownEffects { get; set; }
            public List<Field> Physics { get; set; }
            public List<Field> ClearColours { get; set; }
            public List<Field> EnvFooter { get; set; }
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
