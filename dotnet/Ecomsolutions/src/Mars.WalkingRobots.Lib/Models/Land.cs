using System.Collections.Generic;

namespace Mars.WalkingRobots.Lib.Models
{
    public class Land
    {
        public List<Point> DeadEnds { get; set; }

        public int XBound { get; }
        public int YBound { get; }

        public Land(int xBound, int yBound)
        {
            DeadEnds = new List<Point>();

            XBound = xBound;
            YBound = yBound;
        }

        public bool IsDeadEnd(Point point)
        {
            return DeadEnds.Contains(point);
        }

        public void AddDeadEnd(Point point)
        {
            DeadEnds.Add(point);
        }
    }
}
