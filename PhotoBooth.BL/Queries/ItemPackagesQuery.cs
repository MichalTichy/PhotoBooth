
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Facades;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.BL.Queries
{
    public class ItemPackagesQuery : QueryBase<ItemPackage, ItemPackageDTO>
    {
        public ItemPackagesQuery(string dbName = "") : base(dbName) { }
    }
}
