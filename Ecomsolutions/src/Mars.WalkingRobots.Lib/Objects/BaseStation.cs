using System;
using System.Collections.Generic;
using Mars.WalkingRobots.Lib.Models;

namespace Mars.WalkingRobots.Lib.Objects
{
    public class BaseStation
    {
        private Land _map;
        public readonly Queue<Robot> Robots;
        public readonly List<Robot> RobotsAlive;
        public readonly List<Robot> RobotsDead;

        public BaseStation()
        {
            Robots = new Queue<Robot>();
            RobotsAlive = new List<Robot>();
            RobotsDead = new List<Robot>();
        }

        public BaseStation(Queue<Robot> robots)
        {
            Robots = robots;
            RobotsAlive = new List<Robot>();
            RobotsDead = new List<Robot>();
        }

        private static bool TryParse(string input)
        {
            bool isGood = true;

            try
            {
                string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                string[] inputsMap = lines[0].Trim().Split(' ');
                if (!int.TryParse(inputsMap[0], out var xBound))
                {
                    return false;
                }

                if (!int.TryParse(inputsMap[1], out var yBound))
                {
                    return false;
                }

                if (yBound > 50 || xBound > 50)
                {
                    throw new ArgumentException("Max map length exceeded");
                }
                
                if (lines.Length < 2)
                {
                    throw new ArgumentException("No enough lines");
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Length > 100)
                    {
                        throw new ArgumentException("Max instruction length exceeded");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isGood = false;
            }

            return isGood;
        }

        public void Init(string input)
        {
            if(!TryParse(input))
                return;

            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            string[] inputsMap = lines[0].Trim().Split(' ');
            int xBound = int.Parse(inputsMap[0]);
            int yBound = int.Parse(inputsMap[1]);

            _map = new Land(xBound, yBound);


            Robot robot = null;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if(string.IsNullOrEmpty(line))
                    continue;

                if (i % 2 == 0)
                {
                    if (robot != null)
                    {
                        foreach (var character in line)
                        {
                            robot.AddCommand(Utils.ConvertToCommand(character));
                        }

                        Robots.Enqueue(robot);
                    }
                }
                else
                {
                    string[] inputsPoint = line.Split(' ');
                    int xPos = int.Parse(inputsPoint[0]);
                    int yPos = int.Parse(inputsPoint[1]);
                    string orient = inputsPoint[2].Trim();

                    robot = new Robot(xPos, yPos, Utils.ConvertToOrientation(orient[0]), _map);
                }
            }
        }

        public void Run()
        {
            while (Robots.Count > 0)
            {
                var robot = Robots.Dequeue();
                robot.ExecuteCommands();
                robot.PrintReport();

                if (!robot.IsLost)
                {
                    RobotsAlive.Add(robot);
                }
                else
                {
                    RobotsDead.Add(robot);
                }
            }
        }
    }
}
