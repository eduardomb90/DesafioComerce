using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.ContextConfiguration
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Active)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithOne(x => x.Supplier)
                .HasForeignKey("AddressId")
                .IsRequired();            
            
            builder.HasOne(x => x.Email)
                .WithOne(x => x.Supplier)
                .HasForeignKey("EmailId")
                .IsRequired();
            builder.Property(x => x.Email)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.InserDate)
                .IsRequired()
                .HasColumnType("date");
        }
    }
}
