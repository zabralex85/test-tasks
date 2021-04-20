using System.Collections.Generic;
using System.Linq;
using Mars.WalkingRobots.Lib.Models;
using Mars.WalkingRobots.Lib.Objects;
using NUnit.Framework;

namespace Mars.WalkingRobots.Tests
{
    public class BaseStationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CommandSequenceTest()
        {
            //arrange
            var mocks = new Mocks();
            var mockRobot = mocks.GetRobot();

            var commands = mocks.GetCommandSequence();
            foreach (var command in commands)
            {
                mockRobot.AddCommand(command);
            }
            
            var robots = new Queue<Robot>();
            robots.Enqueue(mockRobot);

            var baseStation = new BaseStation(robots);
            baseStation.Run();

            //assert
            var robot = baseStation.RobotsDead.First();
            Assert.AreEqual(3, robot.CurrentPosition.CurrentPoint.X);
            Assert.AreEqual(3, robot.CurrentPosition.CurrentPoint.Y);
            Assert.AreEqual(Orientation.North, robot.CurrentPosition.CurrentOrientation);
            Assert.AreEqual(true, robot.IsLost);
        }
    }
}