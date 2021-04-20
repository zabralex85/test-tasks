using System.IO;
using System.Reflection;
using Alemira.RandomData.Lib.Collections;
using Alemira.RandomData.Lib.Interfaces;
using Xunit;

namespace Alemira.RandomData.Tests
{
    public class SimpleStringCollectionTest
    {
        private readonly string _fileOnePath;
        private readonly string _fileSecondPath;

        public SimpleStringCollectionTest()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";
            _fileOnePath = Path.Combine(basePath, "TestData\\fileOne.txt");
            _fileSecondPath = Path.Combine(basePath, "TestData\\fileSecond.txt");
        }

        [Fact]
        public void CanInitWithFile()
        {
            var lines = File.ReadAllLines(_fileOnePath);
            
            ICustomCollection collection = new SimpleStringCollection(_fileOnePath);
            Assert.True(collection.Count == lines.Length);
        }

        [Fact]
        public void CanAppendAnotherFile()
        {
            var lines = File.ReadAllLines(_fileSecondPath);

            ICustomCollection collection = new SimpleStringCollection(_fileOnePath);
            var counter = collection.Count;

            collection.AppendDataFromFile(_fileSecondPath);

            Assert.True(counter + lines.Length == collection.Count);
        }

        [Fact]
        public void IsStringPresentOrNotCaseSensitive()
        {
            string someRealString = "lvuOxGqpQLWBNUhffFeM0OX8l6pqlqg15VVk0lsvWldf2MeARtNQhpHXXwn1b9EkoeG187o3Xs0Pv1GYboY2cPUPO8ME80TWh21X";
            Assert.NotNull(someRealString);

            string someFakeString = someRealString.ToLowerInvariant();

            ICustomCollection collection = new SimpleStringCollection(_fileOnePath);
            var val = collection.Find(someRealString);
            var valFake = collection.Find(someFakeString);

            Assert.NotNull(val);
            Assert.Null(valFake);
        }
    }
}