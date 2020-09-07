using Mars.WalkingRobots.Lib.Models;
using Mars.WalkingRobots.Lib.Objects;
using NUnit.Framework;

namespace Mars.WalkingRobots.Tests
{
    public class RobotsTest
    {
        [Test]
        public void CheckOnDeadEnd()
        {
            //arrange
            var land = new Land(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, land);
            var savedRobot = new Robot(5, 3, Orientation.North, land);

            //act
            lostRobot.ExecuteCommand(new RobotCommand(CommandType.Forward));
            savedRobot.ExecuteCommand(new RobotCommand(CommandType.Forward));

            //assert
            Assert.AreEqual(true, lostRobot.IsLost);
            Assert.AreEqual(false, savedRobot.IsLost);
        }

        [Test]
        public void ReportLastPositionIfFallenOff()
        {
            //arrange
            var land = new Land(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, land);

            //act
            lostRobot.ExecuteCommand(new RobotCommand(CommandType.Forward));
            lostRobot.ExecuteCommand(new RobotCommand(CommandType.Forward));

            //assert
            //lost robot's position should be the last one if had before falling off
            Assert.AreEqual(5, lostRobot.CurrentPosition.CurrentPoint.X);
            Assert.AreEqual(3, lostRobot.CurrentPosition.CurrentPoint.Y);
        }

        [TestCase(Orientation.North, 3, true, TestName = "LostInTheNorth")]
        [TestCase(Orientation.North, 2, false, TestName = "EdgeOfTheNorth")]
        [TestCase(Orientation.East, 5, true, TestName = "LostInTheEast")]
        [TestCase(Orientation.East, 4, false, TestName = "EdgeOfTheEast")]
        [TestCase(Orientation.South, 2, true, TestName = "LostInTheSouth")]
        [TestCase(Orientation.South, 1, false, TestName = "EdgeOfTheSouth")]
        [TestCase(Orientation.West, 2, true, TestName = "LostInTheWest")]
        [TestCase(Orientation.West, 1, false, TestName = "EdgeOfTheWest")]
        public void CheckBoundaries(Orientation orientation, int timesForward, bool shouldBeLost)
        {
            //arrange
            var land = new Land(5, 3);
            var robot = new Robot(1, 1, orientation, land);

            //act
            for (int i = 0; i < timesForward; i++)
            {
                robot.ExecuteCommand(new RobotCommand(CommandType.Forward));
            }

            //assert
            Assert.AreEqual(shouldBeLost, robot.IsLost);
        }

        [TestCase(Orientation.North, 1, 2)]
        [TestCase(Orientation.South, 1, 0)]
        [TestCase(Orientation.East, 2, 1)]
        [TestCase(Orientation.West, 0, 1)]
        public void DirectionalMovement(Orientation orientation, int x, int y)
        {
            //arrange
            var land = new Land(5, 3);
            var robot = new Robot(1, 1, orientation, land);

            //act
            robot.ExecuteCommand(new RobotCommand(CommandType.Forward));

            //assert
            Assert.AreEqual(x, robot.CurrentPosition.CurrentPoint.X);
            Assert.AreEqual(y, robot.CurrentPosition.CurrentPoint.Y);
        }

        [TestCase(CommandType.Right, 1, Orientation.East)]
        [TestCase(CommandType.Right, 2, Orientation.South)]
        [TestCase(CommandType.Right, 3, Orientation.West)]
        [TestCase(CommandType.Right, 4, Orientation.North)]
        [TestCase(CommandType.Left, 1, Orientation.West)]
        [TestCase(CommandType.Left, 2, Orientation.South)]
        [TestCase(CommandType.Left, 3, Orientation.East)]
        [TestCase(CommandType.Left, 4, Orientation.North)]
        public void Reorientation(CommandType commandType, int timesRotate, Orientation orientation)
        {
            //arrange
            var land = new Land(5, 3);
            var robot = new Robot(0, 0, Orientation.North, land);

            //act
            for (int i = 0; i < timesRotate; i++)
            {
                robot.ExecuteCommand(new RobotCommand(commandType));
            }

            //assert
            Assert.AreEqual(orientation, robot.CurrentPosition.CurrentOrientation);
            Assert.AreEqual(0, robot.CurrentPosition.CurrentPoint.X);
            Assert.AreEqual(0, robot.CurrentPosition.CurrentPoint.Y);
        }
    }
}