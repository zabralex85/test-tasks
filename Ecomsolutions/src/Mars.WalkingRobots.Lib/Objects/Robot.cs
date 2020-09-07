using System;
using Mars.WalkingRobots.Lib.Models;

namespace Mars.WalkingRobots.Lib.Objects
{
    public class Robot
    {
        public readonly RobotPosition CurrentPosition;
        public bool IsLost;

        private readonly CommandProcessor<RobotCommand> _commandProcessor;
        private readonly RobotPosition _initialPosition;
        private readonly Land _map;

        public Robot(int x, int y, Orientation orientation, Land map)
        {
            _commandProcessor = new CommandProcessor<RobotCommand>();
            _map = map;

            _initialPosition = new RobotPosition
            {
                CurrentOrientation = orientation,
                CurrentPoint = new Point(x, y)
            };

            CurrentPosition = new RobotPosition
            {
                CurrentOrientation = orientation,
                CurrentPoint = new Point(x, y)
            };
        }

        public void ExecuteCommand(RobotCommand robotCommand)
        {
            if (IsLost) return;

            switch (robotCommand.CommandType)
            {
                case CommandType.Forward:
                    MoveForward();
                    break;
                case CommandType.Right:
                    Rotate(robotCommand);
                    break;
                case CommandType.Left:
                    Rotate(robotCommand);
                    break;
                default:
                    throw new ArgumentException($"RobotCommand {robotCommand} not recognized");
            }
        }

        private void Rotate(RobotCommand robotCommand)
        {
            if (robotCommand.CommandType == CommandType.Left)
            {
                switch (CurrentPosition.CurrentOrientation)
                {
                    case Orientation.North:
                        CurrentPosition.CurrentOrientation = Orientation.West;
                        break;
                    case Orientation.East:
                        CurrentPosition.CurrentOrientation = Orientation.North;
                        break;
                    case Orientation.South:
                        CurrentPosition.CurrentOrientation = Orientation.East;
                        break;
                    case Orientation.West:
                        CurrentPosition.CurrentOrientation = Orientation.South;
                        break;
                }
            }
            else if (robotCommand.CommandType == CommandType.Right)
            {
                switch (CurrentPosition.CurrentOrientation)
                {
                    case Orientation.North:
                        CurrentPosition.CurrentOrientation = Orientation.East;
                        break;
                    case Orientation.East:
                        CurrentPosition.CurrentOrientation = Orientation.South;
                        break;
                    case Orientation.South:
                        CurrentPosition.CurrentOrientation = Orientation.West;
                        break;
                    case Orientation.West:
                        CurrentPosition.CurrentOrientation = Orientation.North;
                        break;
                }
            }

            _commandProcessor.CommandsExecuted.Add(robotCommand);
        }

        private void MoveForward()
        {
            var nextPosition = GetForwardPosition();

            if (IsOutOfBounds(nextPosition))
            {
                if (!_map.IsDeadEnd(nextPosition))
                {
                    IsLost = true;
                    _map.AddDeadEnd(nextPosition);
                }
            }
            else
            {
                if (!_map.IsDeadEnd(nextPosition))
                {
                    CurrentPosition.CurrentPoint = nextPosition;
                }
            }

            _commandProcessor.CommandsExecuted.Add(new RobotCommand(CommandType.Forward));
        }

        private Point GetForwardPosition()
        {
            Point nextPosition = default;

            switch (CurrentPosition.CurrentOrientation)
            {
                case Orientation.North:
                    nextPosition = new Point(CurrentPosition.CurrentPoint.X, CurrentPosition.CurrentPoint.Y + 1);
                    break;
                case Orientation.East:
                    nextPosition = new Point(CurrentPosition.CurrentPoint.X + 1, CurrentPosition.CurrentPoint.Y);
                    break;
                case Orientation.South:
                    nextPosition= new Point(CurrentPosition.CurrentPoint.X, CurrentPosition.CurrentPoint.Y - 1);
                    break;
                case Orientation.West:
                    nextPosition = new Point(CurrentPosition.CurrentPoint.X - 1, CurrentPosition.CurrentPoint.Y);
                    break;
            }
            
            return nextPosition;
        }

        private bool IsOutOfBounds(Point point)
        {
            var result = 
                point.X > _map.XBound 
                   || point.X < 0 
                   || 
                point.Y > _map.YBound 
                   || point.Y < 0;

            return result;
        }

        public void ExecuteCommands()
        {
            while (_commandProcessor.Commands.Count > 0)
            {
                var command = _commandProcessor.Commands.Dequeue();
                ExecuteCommand(command);
            }
        }

        public void PrintReport()
        {
            Console.WriteLine(
                $"Initial Position:{_initialPosition.CurrentPoint.X} {_initialPosition.CurrentPoint.Y} {_initialPosition.CurrentOrientation.ToString()[0]}");
            
            Console.WriteLine(
                $"Last Position:{CurrentPosition.CurrentPoint.X} {CurrentPosition.CurrentPoint.Y} {CurrentPosition.CurrentOrientation.ToString()[0]},Lost:{IsLost}");

            Console.WriteLine();
        }

        public void AddCommand(RobotCommand command)
        {
            _commandProcessor.Commands.Enqueue(command);
        }
    }
}