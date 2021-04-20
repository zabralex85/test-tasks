using System;
using System.IO;
using System.Text;
using Alemira.RandomData.Lib.Interfaces;
using KTrie;

namespace Alemira.RandomData.Lib.Collections
{
    public class AdvancedStringCollection: StringTrieSet, ICustomCollection
    {
        public AdvancedStringCollection(string filePath) : base()
        {
            if (string.IsNullOrEmpty(filePath)) return;

            AppendDataFromFile(filePath);
        }

        public string Find(string value)
        {
            if (base.Contains(value))
            {
                return value;
            }

            return null;
        }

        public string FindAfter(string value)
        {
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
                    //string key = line.Substring(0, 5);
                    Add(line);
                }
            }
        }
    }
}