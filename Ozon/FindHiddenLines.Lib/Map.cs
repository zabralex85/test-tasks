using System.Collections.Generic;

namespace FindHiddenLines.Lib
{
    public class Map
    {
        public IntPoint MinXY { get; set; }
        public IntPoint MaxXY { get; set; }
        public List<IntPoint> Points { get; set; }

        public Map(int minX, int maxX, int minY, int maxY)
        {
            this.MinXY = new IntPoint(minX, minY);
            this.MaxXY = new IntPoint(maxX, maxY);
            this.Points = new List<IntPoint>(maxX * maxY);
        }

        public void Init(int[] xSeries, int[] ySeries)
        {
            for (int i = 0; i < xSeries.Length; i++)
            {
                this.Points.Add(new IntPoint(xSeries[i], ySeries[i]));
            }
        }
    }
}