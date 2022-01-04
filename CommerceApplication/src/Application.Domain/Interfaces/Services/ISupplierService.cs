using Application.Domain.Entities;
using Application.Domain.Entities.DTO;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Services
{
    public interface ISupplierService
    {
        Task AddSupplier(SupplierDTO supplier);
        Task<Supplier> GetSupplierById(Guid Id);
        Task<PaginationViewModel<Supplier>> Pagination(int PageSize, int PageIndex, string query);
        Task<Supplier> FindById(Guid Id);
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task Update(SupplierDTO entity);
        Task Remove(Guid Id);
    }
}
