using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<Supplier> GetSupplierById(Guid Id);
        Task<PaginationViewModel<Supplier>> Pagination(int PageSize, int PageIndex, string query);
        Task<IEnumerable<Supplier>> GetSuppliers();

        Task AddPhone(Phone phone);

        Task UpdateAddress(Address address);
        Task UpdateEmail(Email email);
        Task UpdatePhone(Phone phone);

        Task RemoveAddress(Address address);
        Task RemoveEmail(Email email);
        Task RemovePhone(Phone phone);
    }
}
