using Application.Data.Context;
using Application.Domain.Entities;
using Application.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CommerceDbContext _commerceDbContext;

        public BaseRepository(CommerceDbContext commerceDbContext)
        {
            _commerceDbContext = commerceDbContext;
        }

        //Methods
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _commerceDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> FindById(Guid Id)
        {
            return await _commerceDbContext.Set<TEntity>().Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Insert(TEntity entity)
        {
            _commerceDbContext.Set<TEntity>().Add(entity);
            await Task.CompletedTask;
        }

        public async Task Remove(TEntity entity)
        {
            _commerceDbContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task Update(TEntity entity)
        {
            _commerceDbContext.Set<TEntity>().Update(entity);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _commerceDbContext?.Dispose();
        }
    }
}
