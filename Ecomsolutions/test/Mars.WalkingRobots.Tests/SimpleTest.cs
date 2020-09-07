using System;
using System.Collections.Generic;
using System.Text;
using Mars.WalkingRobots.Lib;
using Mars.WalkingRobots.Lib.Models;
using Mars.WalkingRobots.Lib.Objects;
using NUnit.Framework;
using NUnit.CompareNetObjects;

namespace Mars.WalkingRobots.Tests
{
    public class SimpleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetMarsRobotsAndCommandSequences()
        {
            var input =
                @"50 30
13 12 N
FRRFLLFFRRFLL
";
            var land = new Land(50, 30);
            var expectedRobots = new Queue<Robot>();
            var expectedCommandSequences = new Mocks().GetCommandSequence();

            var robot = new Robot(13, 12, Orientation.North, land);
            foreach (var sequence in expectedCommandSequences)
            {
                robot.AddCommand(sequence);
            }
            expectedRobots.Enqueue(robot);

            var baseStation = new BaseStation();
            baseStation.Init(input);

            Assert.That(baseStation.Robots, IsDeeplyEqual.To(expectedRobots));
        }

        [Test]
        public void GetRobotReport()
        {
            var robots = new List<Robot> { new Mocks().GetRobot() };
            var robotReport = new StringBuilder();

            foreach (var robot in robots)
            {
                var line = $"{robot.CurrentPosition.CurrentPoint.X} {robot.CurrentPosition.CurrentPoint.Y} {Utils.ConvertToOrientationChar(robot.CurrentPosition.CurrentOrientation)}";
                if (robot.IsLost)
                    line += " LOST";

                robotReport.AppendLine(line);
            }

            Assert.AreEqual("3 2 N" + Environment.NewLine, robotReport.ToString());
        }
    }
}