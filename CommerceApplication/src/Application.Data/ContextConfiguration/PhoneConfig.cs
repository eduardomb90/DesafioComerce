using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.ContextConfiguration
{
    public class PhoneConfig : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Ddd)
                .IsRequired();
            builder.Property(x => x.Number)
                .IsRequired();

            builder.Property(x => x.InsertDate)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(x => x.UpdateDate)
                .HasColumnType("date");
        }
    }
}
