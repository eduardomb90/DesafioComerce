using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Application.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository, IDisposable
    {
        public ProductRepository(CommerceDbContext commerceDbContext) 
            : base(commerceDbContext)
        {
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            return await _commerceDbContext.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Include(x => x.Images)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _commerceDbContext.Products
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginationViewModel<Product>> Pagination(int PageSize, int PageIndex, string query)
        {
            IPagedList<Product> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _commerceDbContext.Products.Where(x => x.Name.Contains(query) /*|| x.FullName.Contains(query)*/).AsNoTracking().ToPagedListAsync(PageIndex, PageSize);
            }
            else
            {
                list = await _commerceDbContext.Products
                                .Include(x => x.Supplier)
                                .Include(x => x.Category)
                                .Include(x => x.Images)
                                .AsNoTracking()
                                .ToPagedListAsync(PageIndex, PageSize);
            }

            return new PaginationViewModel<Product>()
            {
                List = list.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }

        public async Task AddImage(Image image)
        {
            _commerceDbContext.Images.Add(image);
            await Task.CompletedTask;
        }

        public async Task RemoveCategory(Category category)
        {
            _commerceDbContext.Categories.Remove(category);
            await Task.CompletedTask;
        }

        public async Task RemoveImage(Image image)
        {
            _commerceDbContext.Images.Remove(image);
            await Task.CompletedTask;
        }

        public Guid RemoveImageById(Guid id)
        {
            var image = _commerceDbContext.Images.Where(x => x.Id == id).FirstOrDefault();
            var productId = image.ProductId;
            _commerceDbContext.Images.Remove(image);
            return productId;
        }

        public async Task UpdateCategory(Category category)
        {
            _commerceDbContext.Entry(category).State = EntityState.Modified;
            _commerceDbContext.Categories.Update(category);
            await Task.CompletedTask;
        }

        public async Task UpdateImage(Image image)
        {
            _commerceDbContext.Entry(image).State = EntityState.Modified;
            _commerceDbContext.Images.Update(image);
            await Task.CompletedTask;
        }
    }
}

