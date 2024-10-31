using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENVParser.Fields
{
    internal class P5ROnlyFieldsProvider
    {
        private static readonly HashSet<string> _P5RUniqueFields =
            [
            "Field1CD",
            "Field1CE",
            "Field1CF",
            "Field1D0",
            "SceneWhiteLevels",
            "SceneDarkLevels",
            "Field1F0",
            "Field1F4",
            "Field1F8",
            "Field1FC",
            "Field200",
            "Field204",
            "Field208",
            "Field20C",
            "Field210",
            "Field214",
            "RedColourBoost",
            "GreenColourBoost",
            "BlueColourBoost",
            "Field224",
            "Field228",
            "Field22C",
            "Field230",
            "Field234",
            "Field238",
            "Field23C",
            "Field255",
            "ShadowColourRed",
            "ShadowColourGreen",
            "ShadowColourBlue",
            "ShadowColourAlpha",
            "Field328",
            "Field32C",
            "Field330",
            "Field334",
            "Field338",
        ];

        public static HashSet<string> GetP5RUniqueFields() { return _P5RUniqueFields; }
    }
}
