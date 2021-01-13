using System;
using System.IO;
using System.Text;

namespace Alemira.RandomData.Lib
{
    public class Generator
    {
        private readonly Random _random;

        public Generator(Random random = null)
        {
            if (random != null)
            {
                _random = random;
            }
            else
            {
                _random = new Random();
            }
        }

        public void Generate(int maxStrings, int maxLenOfString, string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < maxStrings; i++)
                {
                    sw.WriteLine(GenerateRandomString(maxLenOfString));
                }
            }
        }

        /// <summary>
        /// Generates Random string, for secure strings - use RNGCryptoServiceProvider
        /// </summary>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        private string GenerateRandomString(int maxLen)
        {
            if (maxLen < 0)
                throw new ArgumentException("length must not be negative", "length");

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < maxLen; i++)
            {
                result.Append(chars[_random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}
