using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Data.Context
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSets



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(256)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Entity<SupplierPhysical>().ToTable("SupplierPhysical");
            modelBuilder.Entity<SupplierJuridical>().ToTable("SupplierJuridical");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommerceDbContext).Assembly);
        }
    }
}
