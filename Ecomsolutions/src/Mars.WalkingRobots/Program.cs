using System;
using System.Text;
using Mars.WalkingRobots.Lib.Objects;

namespace Mars.WalkingRobots.Sample
{
    internal static class Program
    {
        private static bool _isInputEnd;

        private static void Main(string[] args)
        {
            var input = GetUserInput();

            var baseStation = new BaseStation();
            baseStation.Init(input);
            baseStation.Run();

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        private static string GetUserInput()
        {
            var userInput = new StringBuilder();
            Console.WriteLine("Input upper-right Mars coordinates, robot positions, and robot instructions. One per line. Press CTRL+C and Enter when finished");
            Console.CancelKeyPress += ConsoleCancelHandler;

            while (!_isInputEnd)
            {
                string line = Console.ReadLine(); 
                if (!string.IsNullOrWhiteSpace(line))
                {
                    userInput.AppendLine(line);
                }
            }
            return userInput.ToString();
        }

        private static void ConsoleCancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            _isInputEnd = true;
        }
    }
}