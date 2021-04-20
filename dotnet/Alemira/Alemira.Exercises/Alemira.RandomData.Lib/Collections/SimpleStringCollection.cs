using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alemira.RandomData.Lib.Interfaces;

namespace Alemira.RandomData.Lib.Collections
{

    public class SimpleStringCollection : List<string>, ICustomCollection
    {
        public SimpleStringCollection(string filePath)
        {
            if(string.IsNullOrEmpty(filePath)) return;

            AppendDataFromFile(filePath);
        }

        public void AppendDataFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) 
                throw new ArgumentException("File path is null");

            if (!File.Exists(filePath))
                throw new ArgumentException("File not exists");

            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Add(line);
                }
            }
        }

        public string Find(string value)
        {
            return base.Find(x => string.Equals(value, x, StringComparison.InvariantCulture));
        }

        public string FindAfter(string value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            int index = base.FindIndex(x => string.Equals(value, x, StringComparison.InvariantCulture));
            if (index != -1)
            {
                base.Sort(index + 1, this.Count - index - 1, StringComparer.Ordinal);
                return this[index + 1];
            }

            return null;
        }
    }
}
