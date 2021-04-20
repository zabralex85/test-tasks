using System;
using Mars.WalkingRobots.Lib.Models;

namespace Mars.WalkingRobots.Lib
{
    public static class Utils
    {
        public static RobotCommand ConvertToCommand(char command)
        {
            RobotCommand commandResult;

            switch (command)
            {
                case 'F':
                    commandResult = new RobotCommand(CommandType.Forward);
                    break;
                case 'R':
                    commandResult = new RobotCommand(CommandType.Right);
                    break;
                case 'L':
                    commandResult = new RobotCommand(CommandType.Left);
                    break;
                default:
                    throw new ArgumentException($"char {command} not recognized");
            }

            return commandResult;
        }

        public static char ConvertToOrientationChar(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return 'N';
                case Orientation.South:
                    return 'S';
                case Orientation.East:
                    return 'E';
                case Orientation.West:
                    return 'W';
                default:
                    throw new ArgumentException($"orientation {orientation} has no defined char equivalent");
            }
        }

        public static Orientation ConvertToOrientation(char command)
        {
            switch (command)
            {
                case 'N':
                    return Orientation.North;
                case 'E':
                    return Orientation.East;
                case 'S':
                    return Orientation.South;
                case 'W':
                    return Orientation.West;
                default:
                    throw new ArgumentException($"char {command} not recognized");
            }
        }
    }
}