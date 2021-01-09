using System;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Facades
{
    public abstract class FacadeBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected UnitOfWorkProvider provider;
        public FacadeBase(UnitOfWorkProvider provider)
        {
            this.provider = provider; 
        }
    }
}
