using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RegisterAvtionSAfterCommit(Action action);
    }
}
