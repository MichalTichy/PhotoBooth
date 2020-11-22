using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace PhotoBooth.DAL.Repository
{
    public class BaseRepository<TEntity> : EntityFrameworkRepository<TEntity,Guid,PhotoBoothContext> where TEntity : class, IEntity<Guid>, new()
    {

        public BaseRepository(IUnitOfWorkProvider unitOfWorkProvider, IDateTimeProvider dateTimeProvider) : base(unitOfWorkProvider, dateTimeProvider)
        {
            
        }
    }
}