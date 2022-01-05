using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Entities.Pagination;
using Application.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Application.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository, IDisposable
    {
        public CategoryRepository(CommerceDbContext commerceDbContext) : base(commerceDbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _commerceDbContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid Id)
        {
            return await _commerceDbContext.Categories
                .Include(x => x.Products)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PaginationViewModel<Category>> Pagination(int PageSize, int PageIndex, string query)
        {
            IPagedList<Category> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _commerceDbContext.Categories.Where(x => x.Name.Contains(query)).AsNoTracking().ToPagedListAsync(PageIndex, PageSize);
            }
            else
            {
                list = await _commerceDbContext.Categories
                                .Include(x => x.Products)
                                .AsNoTracking()
                                .ToPagedListAsync(PageIndex, PageSize);
            }

            return new PaginationViewModel<Category>()
            {
                List = list.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }
    }
}
