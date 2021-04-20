namespace FindHiddenLines.Lib
{
    public class Line
    {
        public IntPoint? StartPoint { get; set; }
        public IntPoint? EndPoint { get; set; }

        public Line(IntPoint? startPoint, IntPoint? endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}