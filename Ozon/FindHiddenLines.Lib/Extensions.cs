using System.Collections.Generic;
using System.Linq;

namespace FindHiddenLines.Lib
{
    public static class Extensions
    {
        public static IEnumerable<Line> GetLinesWithScan(this Map map)
        {
            var tmpLines = new List<Line>();

            for (int y = map.MinXY.Y; y < map.MaxXY.Y; y++)
            {
                var pointsOnLine = 
                    map.Points.Where(p => p.Y == y)
                              .OrderBy(p => p.X).ToList();
                
                if (pointsOnLine.Count <= 1) continue;

                for (int j = 0; j < pointsOnLine.Count; j++)
                {
                    int nextPosition = j + 1;
                    if (nextPosition >= pointsOnLine.Count) continue;
                    
                    int diff = pointsOnLine[j].X + pointsOnLine[nextPosition].X;
                    if (diff % 2 != 0) continue;
                    
                    int x = diff / 2;
                    var point = new IntPoint(x, y);

                    var startCrossPointExists = map.Points.Any(p => p.X == x && p.Y == y);
                    if (startCrossPointExists) continue;

                    var exLine = tmpLines.FirstOrDefault(p => p.StartPoint?.X == x);
                    if (exLine == null)
                    {
                        tmpLines.Add(new Line(point, null));
                    }
                    else
                    {
                        var endCrossPointExists = map.Points.Any(p => p.X == x && p.Y <= y);
                        if (endCrossPointExists) continue;

                        if (point.Y < exLine.StartPoint?.Y)
                        {
                            exLine.StartPoint = point;
                        }
                        else
                        {
                            if (exLine.EndPoint == null)
                            {
                                exLine.EndPoint = point;
                            }
                            else
                            {
                                if (point.Y > exLine.EndPoint?.Y)
                                {
                                    exLine.EndPoint = point;
                                }
                            }
                        }
                    }
                }
            }

            return tmpLines.Where(p => p.EndPoint != null);
        }
    }
}