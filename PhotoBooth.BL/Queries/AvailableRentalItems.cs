using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    class AvailableRentalItems : QueryBase<RentalItem, RentalItemModel>
    {
        private DateTime OrderSince;
        private DateTime OrderTill;
        public AvailableRentalItems(IUnitOfWorkProvider unitOfWorkProvider, DateTime OrderSince, DateTime OrderTill) : base(unitOfWorkProvider)
        {
            this.OrderSince = OrderSince;
            this.OrderTill = OrderTill;
        }
        protected override IQueryable<RentalItemModel> GetQueryable()
        {
            var temp = Context.Orders
                .Where(x => x.RentalSince >= OrderSince && x.RentalTill <= OrderTill)
                .Select(x => x.RentalItems);
            return Context.RentalItems
                .Where(x => temp.Any(y => y.Any(z => x.Id ==z.Id )))
                .ProjectTo<RentalItemModel>(MapConfig); ;
        }

        public void setTimeCriteria(DateTime since, DateTime till)
        {
            OrderSince = since;
            OrderTill = till;
        }
    }
}
