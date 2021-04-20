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
using System.Linq;
using System.Reflection;
using Alemira.RandomData.Lib;
using Alemira.RandomData.Lib.Collections;
using Alemira.RandomData.Lib.Interfaces;
using KTrie;

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
            
            _generator.Generate(100000, 100, fileOnePath); 
            _generator.Generate(100000, 100, fileSecondPath);

            string someRealString = Utils.GetRandomStringFromFile(fileOnePath, _random);
            Console.WriteLine($"Real String:{someRealString}");
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("List<string>");
            TestSimpleStringCollection(fileOnePath, fileSecondPath, someRealString);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("SortedSet<string>");
            TestSimpleSortedStringCollection(fileOnePath, fileSecondPath, someRealString);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("AdvancedCollection<string>");
            TestAdvancedCollection(fileOnePath, fileSecondPath, someRealString);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("FastSearchStringCollection<string>");
            TestFastSearchStringCollection(fileOnePath, fileSecondPath, someRealString);
            Console.WriteLine(Environment.NewLine);

            File.Delete(fileOnePath);
            File.Delete(fileSecondPath);
        }

        private static void TestFastSearchStringCollection(string fileOnePath, string fileSecondPath, string someRealString)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ICustomCollection collection = new FastSearchStringCollection(fileOnePath);

            sw.Stop();
            Console.WriteLine($"Init Collection Time: {sw.Elapsed.TotalMilliseconds}");

            TestAppendAndSearchInCollection(collection, fileSecondPath, someRealString);
        }

        /// <summary>
        /// Maybe can use Trie .. need investigation ...
        /// https://en.wikipedia.org/wiki/Trie
        /// https://github.com/kpol/trie
        /// </summary>
        /// <param name="fileOnePath"></param>
        /// <param name="fileSecondPath"></param>
        /// <param name="someRealString"></param>
        private static void TestAdvancedCollection(string fileOnePath, string fileSecondPath, string someRealString)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ICustomCollection collection = new AdvancedStringCollection(fileOnePath);

            sw.Stop();
            Console.WriteLine($"Init Collection Time: {sw.Elapsed.TotalMilliseconds}");

            TestAppendAndSearchInCollection(collection, fileSecondPath, someRealString);
        }

        private static void TestSimpleSortedStringCollection(string fileOnePath, string fileSecondPath, string someRealString)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ICustomCollection collection = new SimpleSortedStringCollection(fileOnePath);

            sw.Stop();
            Console.WriteLine($"Init Collection Time: {sw.Elapsed.TotalMilliseconds}");

            TestAppendAndSearchInCollection(collection, fileSecondPath, someRealString);
        }

        private static void TestSimpleStringCollection(string fileOnePath, string fileSecondPath, string someRealString)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ICustomCollection collection = new SimpleStringCollection(fileOnePath);

            sw.Stop();
            Console.WriteLine($"Init Collection Time: {sw.Elapsed.TotalMilliseconds}");

            TestAppendAndSearchInCollection(collection, fileSecondPath, someRealString);
        }

        private static void TestAppendAndSearchInCollection(ICustomCollection collection, string filePath, string searchValue)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            collection.AppendDataFromFile(filePath);
            Console.WriteLine($"Append Collection Time: {sw.Elapsed.TotalMilliseconds}");
            sw.Stop();

            sw.Reset();
            sw.Start();

            var entry = collection.Find(searchValue);
            Console.WriteLine($"entry:{entry}");
            Console.WriteLine($"Find in Collection Time: {sw.Elapsed.TotalMilliseconds}");

            sw.Stop();
            sw.Reset();
            sw.Start();

            var nextEntry = collection.FindAfter(searchValue);
            Console.WriteLine($"next entry:{nextEntry}");

            sw.Stop();
            Console.WriteLine($"FindAfter: {sw.Elapsed.TotalMilliseconds}");
        }
    }
}
