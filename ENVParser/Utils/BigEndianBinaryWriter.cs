using System.Text;

namespace ENVParser.Utils
{

    // This class is required since x86 write LE by default, meaning that the read values are flipped
    // There is an corresponding read function to handle reading the file.
    // The class overrides the BinaryWriter base class for UInt32 and float primitive types
    // If other types are later discovered, this is the entry point to handle write operations for them
    public class BigEndianBinaryWriter : BinaryWriter
    {
        public BigEndianBinaryWriter(Stream output) : base(output) { }

        public BigEndianBinaryWriter(Stream output, Encoding encoding) : base(output, encoding) { }

        public override void Write(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }

        public override void Write(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            base.Write(bytes);
        }
    }
}
