using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Data.Context
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSets
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SupplierJuridical> SupplierJuridicals { get; set; }
        public DbSet<SupplierPhysical> SupplierPhysicals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(256)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            // modelBuilder.Entity<Supplier>().ToTable("Supplier");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommerceDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("InsertDate") != null &&
                                                                      x.Entity.GetType().GetProperty("UpdateDate") != null))
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property("InsertDate").CurrentValue = DateTime.Now;
                    entity.Property("UpdateDate").IsModified = false;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entity.Property("InsertDate").IsModified = false;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}

