using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWork;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    class AvailableRentalItems : QueryBase<RentalItem, RentalItemModel>
    {
        public AvailableRentalItems(string databaseLink = "") : base(databaseLink) { }

        public override ICollection<RentalItem> ExecuteAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var usedItems = uow.OrderRepository
                IQueryable temp = uow.GetRepo<TStart>().Get(fPredicate, sortLambda, "").Take(pageSize).AsQueryable();
                return (ICollection<RentalItem>)temp.ProjectTo<RentalItem>(MapConfig);
            }
        }

    }
}
