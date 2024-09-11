namespace ENVParser.Fields
{
    internal class RGBFieldNameProvider
    {
        private static readonly HashSet<string> _validFields =
        [
            "FieldModelAmbientRed",
            "FieldModelAmbientGreen",
            "FieldModelAmbientBlue",
            "FieldModelAmbientAlpha",
            "FieldModelDiffuseRed",
            "FieldModelDiffuseGreen",
            "FieldModelDiffuseBlue",
            "FieldModelDiffuseAlpha",
            "FieldModelSpecularRed",
            "FieldModelSpecularGreen",
            "FieldModelSpecularBlue",
            "FieldModelSpecularAlpha",
            "FieldModelEmissiveRed",
            "FieldModelEmissiveGreen",
            "FieldModelEmissiveBlue",
            "FieldModelEmissiveAlpha",
            "CharacterModelAmbientRed",
            "CharacterModelAmbientGreen",
            "CharacterModelAmbientBlue",
            "CharacterModelAmbientAlpha",
            "CharacterModelDiffuseRed",
            "CharacterModelDiffuseGreen",
            "CharacterModelDiffuseBlue",
            "CharacterModelSpecularRed",
            "CharacterModelSpecularGreen",
            "CharacterModelSpecularBlue",
            "CharacterModelSpecularAlpha?",
            "CharacterModelEmissiveRed",
            "CharacterModelEmissiveGreen",
            "CharacterModelEmissiveBlue",
            "CharacterModelEmissiveAlpha?",
            "FogRed",
            "FogGreen",
            "FogBlue",
            "FogAlpha",
            "FloorFogRed",
            "FloorFogGreen",
            "FloorFogBlue",
            "RedColourBoost",
            "GreenColourBoost",
            "BlueColourBoost",
            "ShadowColourRed",
            "ShadowColourGreen",
            "ShadowColourBlue",
            "ShadowColourAlpha"
        ];

        public static HashSet<string> GetValidFields() { return _validFields; }
    }
}
