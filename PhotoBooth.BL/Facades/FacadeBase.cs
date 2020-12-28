using System;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;

namespace PhotoBooth.BL.Facades
{
    public abstract class FacadeBase<TEntity> where TEntity : class, IEntity, new()
    {
    }
}
