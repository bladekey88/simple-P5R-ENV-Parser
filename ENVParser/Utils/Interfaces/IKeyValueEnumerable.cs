namespace ENVParser.Utils.Interfaces
{
    internal interface IKeyValueEnumerable
    {
        IEnumerator<KeyValuePair<string, object>> GetEnumerator();
    }
}
