using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alemira.RandomData.Lib.Interfaces;

namespace Alemira.RandomData.Lib.Collections
{
    public class SimpleSortedStringCollection : SortedSet<string>, ICustomCollection
    {
        public SimpleSortedStringCollection(string filePath):base(new CaseSensitiveComparator())
        {
            if (string.IsNullOrEmpty(filePath)) return;

            AppendDataFromFile(filePath);
        }

        public string Find(string value)
        {
            if (base.Contains(value))
                return value;

            return null;
        }

        public string FindAfter(string value)
        {
            bool foundValue = false;

            foreach (var item in this)
            {
                if (foundValue)
                {
                    return item;
                }

                if (string.Equals(item, value, StringComparison.InvariantCulture))
                {
                    foundValue = true;
                }
            }

            return null;
        }

        public void AppendDataFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("File path is null");

            if (!File.Exists(path))
                throw new ArgumentException("File not exists");

            using (var sr = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Add(line);
                }
            }
        }
    }
}