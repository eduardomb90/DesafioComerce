using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(CommerceDbContext commerceDbContext) : base(commerceDbContext)
        {
        }


    }
}
