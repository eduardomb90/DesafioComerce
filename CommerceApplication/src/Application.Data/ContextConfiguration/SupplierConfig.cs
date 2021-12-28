using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .WithOne(x => x.Supplier);

            builder.HasOne(x => x.Email)
                .WithOne(x => x.Supplier);

            builder.HasMany(x => x.Phones)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);

            builder.Property(x => x.InsertDate)
                .IsRequired()
                .HasColumnType("date");
            builder.Property(x => x.UpdateDate)
                .HasColumnType("date");
        }
    }
}
