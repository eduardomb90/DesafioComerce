using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FindById(Guid Id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);        
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
