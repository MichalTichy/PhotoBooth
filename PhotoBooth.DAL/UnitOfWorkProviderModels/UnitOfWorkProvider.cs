
using PhotoBooth.DAL.UnitOfWorkModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWorkProviderModels
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private readonly string dbString = "Server=(localdb)\\mssqllocaldb;Database=PhotoBooth;Trusted_Connection=True;";
        private IUnitOfWork uow;
        public UnitOfWorkProvider() { }
        public UnitOfWorkProvider(string dbString)
        {
            this.dbString = dbString;
        }

        public IUnitOfWork GetUinOfWork()
        {
            uow = (IUnitOfWork)new UnitOfWork(dbString);
            return uow;
        }

    }
}
