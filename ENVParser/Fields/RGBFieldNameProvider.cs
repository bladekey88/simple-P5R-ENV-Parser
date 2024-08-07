namespace ENVParser.Fields
{
    internal class RGBFieldNameProvider
    {
        private static readonly HashSet<string> _validFields =
        [
            "Field Model Ambient Red",
            "Field Model Ambient Green",
            "Field Model Ambient Blue",
            "Field Model Ambient Alpha",
            "Field Model Diffuse Red",
            "Field Model Diffuse Green",
            "Field Model Diffuse Blue",
            "Field Model DiffuseAlpha",
            "Field Model Specular Red",
            "Field Model Specular Green",
            "Field Model Specular Blue",
            "Field Model Specular Alpha",
            "Field Model Emissive Red",
            "Field Model Emissive Green",
            "Field Model Emissive Blue",
            "Field Model Emissive Alpha",
            "Character Model Ambient Red",
            "Character Model Anmbient Green",
            "Character Model Ambient Blue",
            "Character Model Ambient Alpha",
            "Character Model Diffuse Red",
            "Character Model Diffuse Green",
            "Character Model Diffuse Blue",
            "Character Model Specular Red",
            "Character Model Specular Green",
            "Character Model Specular Blue",
            "Character Model Specular Alpha?",
            "Character Model Emissive Red",
            "Character Model Emissive Green",
            "Character Model Emissive Blue",
            "Character Model Emissive Alpha?",
            "Fog Red",
            "Fog Green",
            "Fog Blue",
            "Floor Fog Red",
            "Floor Fog Green",
            "Floor Fog Blue",
            "Red Colour Boost",
            "Green Colour Boost",
            "Blue Colour Boost",
            "Shadow Colour Red",
            "Shadow Colour Green",
            "Shadow Colour Blue",
            "Shadow Colour Alpha"
        ];

        public static HashSet<string> GetValidFields() { return _validFields; }
    }
}
