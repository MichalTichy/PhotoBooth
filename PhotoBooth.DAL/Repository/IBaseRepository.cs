using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(Guid Id);
        Task<Guid> Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(Guid id);
    }
}