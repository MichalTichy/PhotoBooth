using System;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure;

namespace PhotoBooth.BL.Facades
{
    public abstract class FacadeBase<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        protected readonly BaseRepository<TEntity> _repository;
        protected readonly IUnitOfWorkProvider UnitOfWorkFactory;
        public FacadeBase(BaseRepository<TEntity> repository, IUnitOfWorkProvider uow)
        {
            _repository = repository;
            UnitOfWorkFactory = uow;
        }
    }
}
