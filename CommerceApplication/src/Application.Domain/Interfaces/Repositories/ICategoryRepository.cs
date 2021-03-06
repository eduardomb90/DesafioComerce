using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface  ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryById(Guid Id);
        Task<PaginationViewModel<Category>> Pagination(int PageSize, int PageIndex, string query);
        Task<IEnumerable<Category>> GetCategories();
    }
}
