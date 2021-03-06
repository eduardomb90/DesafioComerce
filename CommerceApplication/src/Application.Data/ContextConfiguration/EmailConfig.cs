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
            builder.Property(x => x.EmailAddress)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.InsertDate)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(x => x.UpdateDate)
                .HasColumnType("date");
        }
    }
}
