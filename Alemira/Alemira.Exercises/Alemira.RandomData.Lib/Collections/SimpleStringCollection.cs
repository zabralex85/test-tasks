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

            AppendData(filePath);
        }

        public void AppendData(string filePath)
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
            return base.Find(x => x == value);
        }

        public string FindAfter(string value, bool saveOriginalList = true)
        {
            return this.FindAfter(x => x == value, saveOriginalList);
        }

        public string FindAfter(Predicate<string> match, bool saveOriginalList = true)
        {
            if (saveOriginalList)
                return FindAfterWithCopy(match);
            
            return FindAfterWithoutCopy(match);
        }

        private string FindAfterWithCopy(Predicate<string> match)
        {
            if (match == null)
            {
                throw new NullReferenceException();
            }

            List<string> lastPart = null;
            bool isFound = false;

            for (int i = 0; i < this.Count; i++)
            {
                if (!isFound)
                {
                    if (match(this[i]))
                    {
                        if (this.Count - i > 0)
                        {
                            lastPart = new List<string>(this.Count - i + 1);
                        }

                        isFound = true;
                    }
                }

                lastPart?.Add(this[i]);
            }

            if (lastPart != null)
            {
                if (lastPart.Count > 0)
                {
                    lastPart.Sort(StringComparer.InvariantCultureIgnoreCase);
                    return lastPart[0];
                }
            }

            return null;
        }

        private string FindAfterWithoutCopy(Predicate<string> match)
        {
            if (match == null)
            {
                throw new NullReferenceException();
            }
            
            for (int i = 0; i < this.Count; i++)
            {
                if (match(this[i]))
                {
                    Sort(i + 1, this.Count - i - 1, StringComparer.InvariantCultureIgnoreCase);
                    return this[i + 1];
                }
            }

            return null;
        }
    }
}
