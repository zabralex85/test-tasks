using System.Drawing;
using FindHiddenLines.Lib;
using ScottPlot;

namespace FindHiddenLines.UI.Models
{
    public class SimpleLine : PlottableVLine
    {
        private PointF _startPoint;
        private PointF _endPoint;

        public SimpleLine
        (
            PointF startPoint, 
            PointF endPoint,
            double position, 
            Color color, 
            double lineWidth, 
            string label, 
            bool draggable, 
            double dragLimitLower, 
            double dragLimitUpper, LineStyle lineStyle
        ) 
            : base(position, color, lineWidth, label, draggable, dragLimitLower, dragLimitUpper, lineStyle)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public SimpleLine(IntPoint startPoint, IntPoint endPoint, Color color, int lineWidth, LineStyle lineStyle) 
            : base(0, color, lineWidth, null, false, 0, 0, lineStyle)
        {
            _startPoint = new PointF(startPoint.X, startPoint.Y);
            _endPoint = new PointF(endPoint.X, endPoint.Y);
        }

        public override void Render(Settings settings)
        {
            PointF pixel1 = settings.GetPixel(_startPoint.X, _startPoint.Y);
            PointF pixel2 = settings.GetPixel(_endPoint.X, _endPoint.Y);
            settings.gfxData.DrawLine(this.pen, pixel1, pixel2);
        }
    }
}