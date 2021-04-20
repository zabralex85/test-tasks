using System.Collections.Generic;
using Mars.WalkingRobots.Lib.Models;
using Mars.WalkingRobots.Lib.Objects;

namespace Mars.WalkingRobots.Tests
{
    public class Mocks
    {
        public List<RobotCommand> GetCommandSequence()
        {
            return new List<RobotCommand>
            {
                //FRRFLLFFRRFLL
                new RobotCommand(CommandType.Forward),
                new RobotCommand(CommandType.Right),
                new RobotCommand(CommandType.Right),
                new RobotCommand(CommandType.Forward),
                new RobotCommand(CommandType.Left),
                new RobotCommand(CommandType.Left),
                new RobotCommand(CommandType.Forward),
                new RobotCommand(CommandType.Forward),
                new RobotCommand(CommandType.Right),
                new RobotCommand(CommandType.Right),
                new RobotCommand(CommandType.Forward),
                new RobotCommand(CommandType.Left),
                new RobotCommand(CommandType.Left)
            };
        }

        public Robot GetRobot()
        {
            return new Robot(3, 2, Orientation.North, GetLand());
        }

        public Land GetLand()
        {
            return new Land(5, 3);
        }
    }
}