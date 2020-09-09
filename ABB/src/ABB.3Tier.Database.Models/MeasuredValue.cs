using System;

namespace ABB.NTier.Database.Models
{
    public class MeasuredValue
    {
        public long     MeasuredValueId { get; set; }
        public long     MotorId         { get; set; }
        public float?   ActualAmper     { get; set; }
        public float?   ActualRevsRpm   { get; set; }
        public float?   ActualPressure  { get; set; }
        public DateTime LastUpdated     { get; set; }
        public Motor    Motor           { get; set; }
    }
}