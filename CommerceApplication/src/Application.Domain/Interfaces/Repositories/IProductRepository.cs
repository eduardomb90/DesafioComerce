using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetProductById(Guid Id);
        Task<PaginationViewModel<Product>> Pagination(int PageSize, int PageIndex, string query);
        Task<IEnumerable<Product>> GetProducts();

        Task AddImage(Image image);

        Task UpdateCategory(Category category);
        Task UpdateImage(Image image);

        Task RemoveCategory(Category category);
        Task RemoveImage(Image image);
        Guid RemoveImageById(Guid id);
    }
}
