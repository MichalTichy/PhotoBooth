using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using System;

namespace PhotoBooth.DAL.UnitOfWorkModels
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        BaseRepository<T> GetRepo<T>() where T : class, IEntity, new();
        //void RegisterAvtionSAfterCommit(Action action);
    }
}
