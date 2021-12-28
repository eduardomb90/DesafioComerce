using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<Supplier> GetSupplierById(Guid Id);
        Task<IEnumerable<Supplier>> GetSuppliers();
    }
}
