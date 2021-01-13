using System;
using System.IO;
using System.Text;

namespace Alemira.RandomData.Lib
{
    public static class Utils
    {
        public static string GetRandomStringFromFile(string filePath, Random random, int minLine = 10000, int maxLine = 50000)
        {
            int rNum = random.Next(minLine, maxLine);

            string line;
            int counter = 0;

            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (counter == rNum)
                    {
                        break;
                    }
                    counter++;
                }
            }

            return line;
        }
    }
}
