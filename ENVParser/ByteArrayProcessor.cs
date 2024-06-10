using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser
{
    internal class ByteArrayProcessor
    {
        public object ProcessByteArray(byte[] byteArray, int startIndex, int length, string fieldType)
        {
            byte[] slicedBytes = new byte[length];
            Array.Copy(byteArray, startIndex, slicedBytes, 0,length);

            switch (fieldType) {
                case "enum Boolean":
                    return BitConverter.ToBoolean(slicedBytes);
                case "f32":
                    // Windows uses LE assembly
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(slicedBytes); // Reverse bytes if little endian
                    }
                    return BitConverter.ToSingle(slicedBytes,0);
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
