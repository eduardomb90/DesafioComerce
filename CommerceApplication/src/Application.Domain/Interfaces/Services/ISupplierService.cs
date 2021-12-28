using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Services
{
    public interface ISupplierService
    {

        Task AddSupplier(Supplier supplier);
        Task<Supplier> GetSupplierById(Guid Id);
        Task<IEnumerable<Supplier>> GetSuppliers();
    }
}
