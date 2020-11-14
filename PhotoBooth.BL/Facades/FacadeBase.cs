using System;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.BL.Facades
{
    public abstract class FacadeBase<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        private readonly BaseRepository<TEntity> _repository;

        public FacadeBase(BaseRepository<TEntity> repository)
        {
            _repository = repository;
        }
    }
}
