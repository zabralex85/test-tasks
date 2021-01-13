using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alemira.RandomData.Lib.Interfaces;

namespace Alemira.RandomData.Lib.Collections
{
    public class SimpleSortedStringCollection : SortedList<long, string>, ICustomCollection
    {
        public SimpleSortedStringCollection(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            AppendData(filePath);
        }

        public void AppendData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path is null");

            if (!File.Exists(filePath))
                throw new ArgumentException("File not exists");

            long counter = this.Count;
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Add(counter, line);
                    counter++;
                }
            }
        }

        public string Find(string value)
        {
            if (this.ContainsValue(value))
                return value;

            return null;
        }

        public string FindAfter(string value, bool saveOriginalList = true)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (string.Equals(this[i], value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return this[i + 1];
                }
            }

            return null;
        }
    }
}
