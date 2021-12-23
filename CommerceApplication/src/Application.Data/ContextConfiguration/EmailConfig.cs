using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.ContextConfiguration
{
    public class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasOne(x => x.Supplier)
                .WithOne(x => x.Email)
                .HasForeignKey<Email>(x => x.SupplierId)
                .IsRequired();
        }
    }
}
