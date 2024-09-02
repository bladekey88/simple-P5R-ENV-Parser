using System.Text;

namespace ENVParser.Utils
{
    // This class is required since x86 reads LE by default, meaning that the values are flipped
    // There is an corresponding write function to handle writing the file.
    // The class overrides the BinaryReader base class for UInt32 and Single primitive types
    // If other types are later discovered, this is the entry point to handle read operations for them


    public class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream input) : base(input) { }
        public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding) { }

        public override uint ReadUInt32()
        {
            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        public override float ReadSingle()
        {
            var bytes = base.ReadBytes(4);
            Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        // There is no need to override byte
        // as that is read without endianness (since its just a byte!)
    }
}
