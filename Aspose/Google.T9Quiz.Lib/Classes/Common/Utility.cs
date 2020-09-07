using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Google.T9Quiz.Lib.Classes.Common
{
    public class Utility
    {
        static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string GetPath(string filePath)
        {
            return AssemblyDirectory + "\\" + filePath;
        }

        public static void WriteOutput(string inFile, string outFile, IWrapper _wrapper)
        {
            var datasetLines = File.ReadAllLines(GetPath(inFile));
            var sb = new StringBuilder();

            int internalIterator = 1;
            for (int i = 0; i < datasetLines.Length; i++)
            {
                if (datasetLines[i] == " ")
                {
                    continue;
                }

                if (datasetLines[i] == "100")
                {
                    sb.Append("Case #" + internalIterator + ": 0" + '\n');
                }
                else
                {
                    sb.Append("Case #" + internalIterator + ": " + _wrapper.GetNumberCodes(datasetLines[i]) + '\n');
                }
                internalIterator++;
            }

            File.WriteAllText(GetPath(outFile), sb.ToString());
        }
    }
}