using ENVParser.Utils;
using ENVParser.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser.ENVFileComponents
{
    // This is the base class that all ENV Section Component classes inherit from
    // It implements the interface, via reflection and makes it available to all child classes
    // This avoids code duplication and makes it much easier to manage

    internal class BaseEnvSection : IKeyValueEnumerable
    {
        public virtual IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {             
            foreach (var propertyInfo in GetType().GetProperties())
            {
                yield return new KeyValuePair<string, object>(propertyInfo.Name, propertyInfo.GetValue(this));
            }
        }        
    }
}
