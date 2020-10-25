using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseRepository()
        {
        }

        public async Task<IList<TEntity>> GetAll()
        {
            using (var dbContext = new PhotoBoothContext())
            {
                return await dbContext.Set<TEntity>().ToListAsync();
            }
        }

        public async Task<TEntity> GetById(Guid Id)
        {
            using (var dbContext = new PhotoBoothContext())
            {
                return await dbContext.Set<TEntity>().FindAsync(Id);
            }
        }

        public async Task<Guid> Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            using (var dbContext = new PhotoBoothContext())
            {
                await dbContext.Set<TEntity>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }

            return entity.Id;
        }

        public async Task Update(TEntity entity)
        {
            using (var dbContext = new PhotoBoothContext())
            {
                dbContext.Set<TEntity>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Remove(Guid id)
        {
            using (var dbContext = new PhotoBoothContext())
            {
                var entity = await dbContext.Set<TEntity>().FindAsync(id);
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}