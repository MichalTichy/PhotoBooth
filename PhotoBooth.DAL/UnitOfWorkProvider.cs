using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.DAL
{
    public class UnitOfWorkProvider : UnitOfWorkProviderBase
    {
        public UnitOfWorkProvider(IUnitOfWorkRegistry registry) : base(registry)
        {
            
        }

        protected override IUnitOfWork CreateUnitOfWork(object parameter)
        {
            return new UnitOfWork();
        }
    }
}
