using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWork
{
    public class BaseRepositoryWraper<TEntity> where TEntity : class, IEntity, new()
    {
        private PhotoBoothContext context;
        private BaseRepository<TEntity> repo;
        public BaseRepository<TEntity> Repository {
            get
            {
                if (repo == null)
                {
                    this.repo = new BaseRepository<TEntity>(context);
                }
                return repo;
            }
        }

        public BaseRepositoryWraper(PhotoBoothContext context)
        {
            this.context = context;
        }


    }
}
