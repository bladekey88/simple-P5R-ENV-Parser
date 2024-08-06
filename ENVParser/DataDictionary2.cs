using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser
{
    internal class DataDictionary2
    {
        public string FieldName { get; set; }
        public int HexAddress { get; set; }
        public int FieldLength { get; set; }
        public string FieldType { get; set; }
    }
}
