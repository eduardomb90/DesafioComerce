using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task<Product> GetProductById(Guid Id);
        Task<PaginationViewModel<Product>> Pagination(int PageSize, int PageIndex, string query);
        Task<Product> FindById(Guid Id);
        Task<IEnumerable<Product>> GetProducts();
        Task Update(Product product);
        Task Remove(Guid Id);
        Guid RemoveImage(Guid Id);
    }
}
