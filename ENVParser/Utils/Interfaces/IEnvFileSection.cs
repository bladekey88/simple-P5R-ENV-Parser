using ENVParser.Fields;

namespace ENVParser.Utils.Interfaces
{
    internal interface IEnvFileSectionVersionSpecific<T>
    {
        public T Read(BigEndianBinaryReader reader, uint GFSVersion, ValidVersionHeaderProvider.GameVersions? GameVersion);
    }

    internal interface IEnvFileSection<T>
    {
        public T Read(BigEndianBinaryReader reader);
    }




}
