using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ABB.NTier.Database.Etl;
using ABB.NTier.Database.Models;
using ABB.NTier.Database.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABB.NTier.Database.Configurations
{
    [ExcludeFromCodeCoverage]
    public class MotorConfiguration : IEntityTypeConfiguration<Motor>
    {
        private const string TableName = "Motor";

        public void Configure(EntityTypeBuilder<Motor> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(t => t.MotorId);
            builder.HasIndex(x => x.MotorId)
                .HasName("MotorId")
                .IsUnique();

            builder.Property(b => b.MotorId)
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(b => b.MotorTypeId)
                .IsRequired()
                .HasColumnType("tinyint");
            
            builder.Property(b => b.LastUpdated)
                .IsRequired(false)
                .HasColumnType("datetime2");
            
            builder.Property(b => b.Displacement)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.Voltage)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.CurrentAmper)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.FuelConsumption)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.MaxPower)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.MaxPressure)
                .IsRequired(false)
                .HasColumnType("float");
            
            builder.Property(b => b.MaxTorque)
                .IsRequired(false)
                .HasColumnType("float");

            builder
                .HasMany(c => c.MeasuredValues)
                .WithOne(e => e.Motor)
                .HasForeignKey(x => x.MotorId);

            var data = GetInitialData();
            builder.HasData(data);
        }

        private static IEnumerable<Motor> GetInitialData()
        {
            List<Motor> motors = new List<Motor>();

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

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var motor = new Motor();

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string columnName = table.Columns[j].ColumnName;
                    var val = table.Rows[i][columnName].ToString();

                    switch (columnName)
                    {
                        case "Type":
                            motor.MotorTypeId = (MotorType) Enum.Parse(typeof(MotorType), val);
                            break;
                        case "Max power (kW)":
                            if(!string.IsNullOrEmpty(val)) motor.CurrentAmper = float.Parse(val);
                            break;
                        case "Voltage (V)":
                            if (!string.IsNullOrEmpty(val)) motor.CurrentAmper = float.Parse(val);
                            break;
                        case "Current (A)":
                            if (!string.IsNullOrEmpty(val)) motor.CurrentAmper = float.Parse(val);
                            break;
                        case "Fuel consumtion per hour(l/h)":
                            if (!string.IsNullOrEmpty(val)) motor.FuelConsumption = float.Parse(val);
                            break;
                        case "Max torque at (rpm)":
                            if (!string.IsNullOrEmpty(val)) motor.MaxTorque = float.Parse(val);
                            break;
                        case "Max presure (bar)":
                            if (!string.IsNullOrEmpty(val)) motor.MaxPressure = float.Parse(val);
                            break;
                        case "Displacement (cm3/rev)":
                            if (!string.IsNullOrEmpty(val)) motor.Displacement = float.Parse(val);
                            break;
                    }
                }

                motors.Add(motor);
            }

            return motors;
        }
    }
}