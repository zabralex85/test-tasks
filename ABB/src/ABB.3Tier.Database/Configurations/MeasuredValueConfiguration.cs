using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ABB.NTier.Database.Etl;
using ABB.NTier.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABB.NTier.Database.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MeasuredValueConfiguration : IEntityTypeConfiguration<MeasuredValue>
    {
        private const string TableName = "MeasuredValue";

        public void Configure(EntityTypeBuilder<MeasuredValue> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(t => t.MeasuredValueId);
            builder.HasIndex(x => x.MeasuredValueId)
                .HasName("MeasuredValueId")
                .IsUnique();

            builder.Property(b => b.MeasuredValueId)
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(b => b.ActualAmper)
                .HasColumnType("float")
                .IsRequired(false)
                .ValueGeneratedNever();

            builder.Property(b => b.ActualPressure)
                .IsRequired(false)
                .HasColumnType("float");

            builder.Property(b => b.ActualRevsRpm)
                .IsRequired(false)
                .HasColumnType("float");

            builder.Property(b => b.LastUpdated)
                .IsRequired(false)
                .HasColumnType("datetime2");

            var data = GetInitialData();
            builder.HasData(data);
        }

        private static IEnumerable<MeasuredValue> GetInitialData()
        {
            List<MeasuredValue> measuredValues = new List<MeasuredValue>();

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

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var measuredValue = new MeasuredValue();

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string columnName = table.Columns[j].ColumnName;
                    var val = table.Rows[i][columnName].ToString();

                    switch (columnName)
                    {
                        case "Motor":
                            measuredValue.MotorId = long.Parse(Regex.Match(val, @"\d").Value);
                            break;
                        case "Time of measurement":
                            if (!string.IsNullOrEmpty(val)) measuredValue.LastUpdated = DateTime.Parse(val);
                            break;
                        case "Actual current (A)":
                            if (!string.IsNullOrEmpty(val)) measuredValue.ActualAmper = float.Parse(val);
                            break;
                        case "Actual pressure (bar)":
                            if (!string.IsNullOrEmpty(val)) measuredValue.ActualPressure = float.Parse(val);
                            break;
                        case "Actual revs. (rpm)":
                            if (!string.IsNullOrEmpty(val)) measuredValue.ActualRevsRpm = float.Parse(val);
                            break;
                    }
                }

                measuredValues.Add(measuredValue);
            }

            return measuredValues;
        }
    }
}