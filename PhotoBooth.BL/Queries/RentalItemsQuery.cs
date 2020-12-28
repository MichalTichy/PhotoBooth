using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.Item.RentalItem;
using Riganti.Utils.Infrastructure.Core;
using AutoMapper.QueryableExtensions;
using PhotoBooth.DAL.UnitOfWork;

namespace PhotoBooth.BL.Queries
{
    public class RentalItemsQuery : QueryBase<RentalItem, RentalItemModel>
    {
        public RentalItemsQuery(string dbName = "") : base(dbName) { }
    }
}
