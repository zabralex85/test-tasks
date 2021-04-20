namespace Alemira.RandomData.Lib.Interfaces
{
    public interface ICustomCollection
    {
        string Find(string value);
        string FindAfter(string value);
        void AppendDataFromFile(string path);
        int Count { get; }
    }
}