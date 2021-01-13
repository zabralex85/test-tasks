using System;

namespace Alemira.RandomData.Lib.Interfaces
{
    public interface ICustomCollection
    {
        string Find(string value);
        string FindAfter(string value, bool saveOriginalList = true);
        void AppendData(string path);
    }
}