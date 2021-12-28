using Application.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FindById(Guid Id);        
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
    }
}
