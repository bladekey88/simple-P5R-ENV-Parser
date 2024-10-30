using ENVParser;

internal interface IExporter
{
    public void Export(string filePath, EnvFile envFile) { }
}