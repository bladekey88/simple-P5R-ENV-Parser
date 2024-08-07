namespace ENVParser
{
    public class FieldTypeConverter
    {
        public object ConvertBytes(string fieldType, byte[] slicedBytes)
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

    }
}
