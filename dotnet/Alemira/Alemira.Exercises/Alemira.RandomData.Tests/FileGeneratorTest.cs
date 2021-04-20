using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Alemira.RandomData.Lib;
using Xunit;

namespace Alemira.RandomData.Tests
{
    public class FileGeneratorTest
    {
        private readonly Random _random = new Random(Environment.TickCount);

        [Fact]
        public void IsFileGenerates()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";
            string filePath = Path.Combine(basePath, "file.txt");

            Generator generator = new Generator(_random);
            generator.Generate(100000, 100, filePath);

            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public void IsFileLinesGenerates()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";
            string filePath = Path.Combine(basePath, "file.txt");

            int maxStrings = 100;
            int maxLengthOfString = 10;

            Generator generator = new Generator(_random);
            generator.Generate(maxStrings, maxLengthOfString, filePath);

            Assert.True(File.Exists(filePath));

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            Assert.True(lines.Length == maxStrings);

            foreach (var line in lines)
            {
                Assert.True(line.Length <= maxLengthOfString);
            }
        }

        [Fact]
        public void IsFileLinesPatternMaskRight()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";
            string filePath = Path.Combine(basePath, "file.txt");

            int maxStrings = 100;
            int maxLengthOfString = 10;

            Generator generator = new Generator(_random);
            generator.Generate(maxStrings, maxLengthOfString, filePath);

            var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            var regex = new Regex($"[a-zA-Z0-9]{{1,{maxLengthOfString}}}", RegexOptions.Singleline);

            foreach (var line in lines)
            {
                Assert.Matches(regex, line);
            }
        }
    }
}
