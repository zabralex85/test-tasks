using System;
using System.Collections.Generic;
using ABB.NTier.Database.Models.Enums;

namespace ABB.NTier.Database.Models
{
    public class Motor
    {
        public long                MotorId         { get; set; }
        public MotorType           MotorTypeId     { get; set; }
        public float?              MaxPower        { get; set; }
        public float?              Voltage         { get; set; }
        public float?              CurrentAmper    { get; set; }
        public float?              FuelConsumption { get; set; }
        public float?              MaxTorque       { get; set; }
        public float?              MaxPressure     { get; set; }
        public float?              Displacement    { get; set; }
        public DateTime?           LastUpdated     { get; set; }
        public List<MeasuredValue> MeasuredValues  { get; set; }
    }
}