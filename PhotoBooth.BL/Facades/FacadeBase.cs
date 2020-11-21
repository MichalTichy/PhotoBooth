using System;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure;

namespace PhotoBooth.BL.Facades
{
    public abstract class FacadeBase<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        private readonly BaseRepository<TEntity> _repository;
        private readonly IUnitOfWorkProvider UnitOfWork;
        public FacadeBase(BaseRepository<TEntity> repository, IUnitOfWorkProvider uow)
        {
            _repository = repository;
            UnitOfWork = uow;
        }
    }
}
