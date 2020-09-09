using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using ABB.NTier.Database.Models;
using ClosedXML.Excel;
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

        private IEnumerable<MeasuredValue> GetInitialData()
        {
            throw new NotImplementedException();
        }
    }
}