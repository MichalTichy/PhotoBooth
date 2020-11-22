using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private PhotoBoothContext Context { get; set;}
        public event EventHandler Disposing;

        public UnitOfWork()
        {
            Context = new PhotoBoothContext();
        }

        UnitOfWork(PhotoBoothContext Context)
        {
            this.Context = Context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Task CommitAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Disposing?.Invoke(this, EventArgs.Empty);
            Context.Dispose();
        }

        public void RegisterAfterCommitAction(Action action)
        {
            Commit();
            action?.Invoke();
        }
    }
}
