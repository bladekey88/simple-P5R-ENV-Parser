﻿namespace ENVParser
{
    internal interface IExporter
    {
        void Export(string filePath, Dictionary<string, (object,string)> extractedData);
    }
}
