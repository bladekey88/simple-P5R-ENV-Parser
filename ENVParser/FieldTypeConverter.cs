namespace ENVParser
{
    public class FieldTypeConverter
    {
        public object ConvertFrom(string fieldType, byte[] slicedBytes)
        {
            if (fieldType.StartsWith("struct", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            switch (fieldType)
            {
                case "enum Boolean":
                    return BitConverter.ToBoolean(slicedBytes);
                case "f32":
                    // Windows uses LE assembly
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(slicedBytes); // Reverse bytes if little endian
                    }
                    return BitConverter.ToSingle(slicedBytes, 0);
                case "u32":
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(slicedBytes); // Reverse bytes if little endian
                    }
                    return BitConverter.ToUInt32(slicedBytes);
                case "u8":
                    return slicedBytes[0];
                default:
                    throw new ArgumentException($"Unsupported Field Type: {fieldType}");
            }
        }

        public static object ConvertToBytes(string fieldType, object fieldValue)
        {
            if (fieldType.StartsWith("struct", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            switch (fieldType)
            {
                case "enum Boolean":
                    return BitConverter.GetBytes((bool)fieldValue);
                case "u8":
                    return (byte)fieldValue;
                case "f32":
                    var floatBytes = BitConverter.GetBytes((float)fieldValue);
                    // Windows uses LE assembly
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(floatBytes); // Reverse bytes if little endian
                    }
                    return floatBytes;
                case "u32":
                    // Windows uses LE assembly
                    var intBytes = BitConverter.GetBytes((int)fieldValue);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(intBytes); // Reverse bytes if little endian
                    }
                    return intBytes;
                default:
                    throw new ArgumentException($"Unsupported Field Type: {fieldType}");
            }
        }

    }
}
