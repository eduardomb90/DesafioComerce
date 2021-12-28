using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.ContextConfiguration
{
    class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Supplier)
                .WithOne(x => x.Address);

            builder.Property(x => x.ZipCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnType("varchar(9)");

            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            builder.Property(x => x.Number)
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("varchar(50)");

            builder.Property(x => x.Complement)
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("varchar(50)");

            builder.Property(x => x.Neighborhood)
               .IsRequired()
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");

            builder.Property(x => x.City)
               .IsRequired()
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");


            builder.Property(x => x.State)
               .IsRequired()
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");

            builder.Property(x => x.Reference)
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");

            
            builder.Property(x => x.InsertDate)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(x => x.UpdateDate)
                .HasColumnType("date");
        }
    }
}

