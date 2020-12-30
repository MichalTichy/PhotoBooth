using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        public TEntity Get(Guid id);
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(Guid id);
    }
}
