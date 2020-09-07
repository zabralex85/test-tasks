using Google.T9Quiz.Lib.Classes.Workers;
using System;

namespace Google.T9Quiz
{
    class Program
    {
        private static T9V3 _wrapper { get; set; }

        private static void Main(string[] args)
        {
            _wrapper = new T9V3();

            if (args.Length != 0)
            {
                Console.WriteLine(_wrapper.GetNumberCodes(args[0]));
            }

            Console.Read();
        }
    }
}
