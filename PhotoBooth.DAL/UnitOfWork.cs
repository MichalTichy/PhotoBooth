using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.DAL
{
    public class UnitOfWork : UnitOfWorkBase
    {
        protected override Task CommitAsyncCore(CancellationToken cancellationToken)
        {
            return this.CommitAsync();
        }

        protected override void CommitCore()
        {
            this.Commit();
        }

        protected override void DisposeCore()
        {
            this.Dispose();
        }
    }
}
