using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABB.NTier.Database.Etl.Tests
{
    [TestClass]
    public class ExtractorTests
    {
        [TestMethod]
        public void MotorsConvertToTable()
        {
            string file = System.IO.Path.Combine(Environment.CurrentDirectory, "Data\\initial.xlsx");
            var table = Extractor.GetInitialData(file, "Motor name",
                new[]
                {
                    "Type",
                    "Max power (kW)",
                    "Voltage (V)", 
                    "Current (A)", 
                    "Fuel consumtion per hour(l/h)", 
                    "Max torque at (rpm)",
                    "Max presure (bar)", 
                    "Displacement (cm3/rev)"
                },
                1);

            Assert.IsNotNull(table);
            Assert.AreEqual(table.Rows.Count, 3);
            Assert.AreEqual("2", table.Rows[0]["Max power (kW)"].ToString());
            Assert.AreEqual("16", table.Rows[2]["Displacement (cm3/rev)"].ToString());
        }

        [TestMethod]
        public void MeasuresConvertToTable()
        {
            string file = System.IO.Path.Combine(Environment.CurrentDirectory, "Data\\initial.xlsx");
            var table = Extractor.GetInitialData(file, "Time of measurement",
                new[]
                {
                    "Motor",
                    "Actual current (A)",
                    "Actual revs. (rpm)",
                    "Actual pressure (bar)"
                },
                2);

            Assert.IsNotNull(table);
            Assert.AreEqual(table.Rows.Count, 21);

            Assert.AreEqual("7", table.Rows[18]["Actual current (A)"].ToString());
            Assert.AreEqual("2900", table.Rows[19]["Actual revs. (rpm)"].ToString());
            Assert.AreEqual("139", table.Rows[20]["Actual pressure (bar)"].ToString());
        }
    }
}
