using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;

namespace PhotoBooth.DAL.Repository
{
    public class BaseRepository<TEntity> : EntityFrameworkRepository<TEntity, Guid, PhotoBoothContext> where TEntity : class, IEntity<Guid>, new()
    {
        public BaseRepository(IUnitOfWorkProvider unitOfWorkProvider, IDateTimeProvider dateTimeProvider) : base(unitOfWorkProvider, dateTimeProvider)
        {
        }
    }
}