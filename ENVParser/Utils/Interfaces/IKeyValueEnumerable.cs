using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser.Utils.Interfaces
{
    internal interface IKeyValueEnumerable
    {
        IEnumerator<KeyValuePair<string,object>> GetEnumerator();
    }
}
