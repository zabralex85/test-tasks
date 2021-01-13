/*
 * Prepare two text files both with 100000 random strings. Every random string has random number of random alphanumeric ([0-9A-z]) symbols up to 100 symbols length
 * Implement own collection that may contain large numbers of strings like described above and
 * 1)	may be initialized by reading from the first file;
 * 2)	data from the second file may be appended to the collection;
 * 3)	implement a method string Find(string s) that returns s if s is contained in the collection or return a string from the collection that is next to s in alphanumeric order;
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Alemira.RandomData.Lib;

namespace Alemira.RandomData.Sample
{
    class Program
    {
        private static Random _random;
        private static Generator _generator;

        static void Main(string[] args)
        {
            _random = new Random(Environment.TickCount);
            _generator = new Generator(_random);

            string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? "";
            string fileOnePath = Path.Combine(basePath, "fileOne.txt");
            string fileSecondPath = Path.Combine(basePath, "fileSecond.txt");

            if (File.Exists(fileOnePath))
                File.Delete(fileOnePath);

            if (File.Exists(fileSecondPath))
                File.Delete(fileSecondPath);

            _generator.Generate(100000, 100, fileOnePath);
            _generator.Generate(100000, 100, fileSecondPath);

            string someRealString = Utils.GetRandomStringFromFile(fileOnePath, _random);
            Console.WriteLine($"Real String:{someRealString}");
            Console.WriteLine(Environment.NewLine);

            TestSimpleStringCollection(fileOnePath, fileSecondPath, someRealString);
        }

        private static void TestSimpleStringCollection(string fileOnePath, string fileSecondPath, string someRealString)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var collectionOne = new SimpleStringCollection(fileOnePath);

            sw.Stop();
            Console.WriteLine($"Init Collection Time: {sw.Elapsed.TotalMilliseconds}");

            sw.Reset();
            sw.Start();

            collectionOne.AppendData(fileSecondPath);
            Console.WriteLine($"Append Collection Time: {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();

            sw.Reset();
            sw.Start();

            var entry = collectionOne.Find(x => x == someRealString);
            Debug.WriteLine($"entry:{entry}");

            var nextEntry = collectionOne.FindAfter(x => x == someRealString, saveOriginalList: true);
            Debug.WriteLine($"next entry with copy:{nextEntry}");

            sw.Stop();
            Console.WriteLine($"FindAfter with saving original list: {sw.Elapsed.TotalMilliseconds}");

            sw.Reset();
            sw.Start();

            var nextEntryWithoutCopy = collectionOne.FindAfter(x => x == someRealString, saveOriginalList: false);
            Debug.WriteLine($"next entry without copy:{nextEntryWithoutCopy}");
            sw.Stop();

            Console.WriteLine($"FindAfter without saving original list: {sw.Elapsed.TotalMilliseconds}");
        }
    }
}
