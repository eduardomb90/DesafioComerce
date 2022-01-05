using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        Task AddCategory(Category category);
        Task<Category> GetCategoryById(Guid Id);
        Task<PaginationViewModel<Category>> Pagination(int PageSize, int PageIndex, string query);
        Task<Category> FindById(Guid Id);
        Task<IEnumerable<Category>> GetCategories();
        Task Update(Category category);
        Task Remove(Guid Id);
    }
}
