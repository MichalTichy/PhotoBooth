
using PhotoBooth.DAL.UnitOfWorkModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWorkProviderModels
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork GetUinOfWork();
    }
}
