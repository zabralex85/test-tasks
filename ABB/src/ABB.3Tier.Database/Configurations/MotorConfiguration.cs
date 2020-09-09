using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ABB.NTier.Database.Models;
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

        private IEnumerable<Motor> GetInitialData()
        {
            throw new System.NotImplementedException();
        }
    }
}