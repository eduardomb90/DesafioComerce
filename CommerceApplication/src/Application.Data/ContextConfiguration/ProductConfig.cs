using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.ContextConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(250)
               .HasColumnType("varchar(250)");

            builder.Property(x => x.BarCode)
               .IsRequired()
               .HasMaxLength(500)
               .HasColumnType("varchar(500)");

            builder.Property(x => x.QuantityStock)
               .IsRequired();

            builder.Property(x => x.PricePurchase)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.PriceSales)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.InsertDate)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(x => x.UpdateDate)
                .HasColumnType("date");
        }
    }
}
