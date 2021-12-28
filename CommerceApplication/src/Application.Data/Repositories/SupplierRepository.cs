using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(CommerceDbContext commerceDbContext) : base(commerceDbContext)
        {
        }

        public async Task<Supplier> GetSupplierById(Guid Id)
        {
            return await _commerceDbContext.Suppliers
                .Include(x => x.Phones)
                .Include(x => x.Email)
                .Include(x => x.Address)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _commerceDbContext.Suppliers
                .Include(x => x.Phones)
                .Include(x => x.Email)
                .Include(x => x.Address)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
