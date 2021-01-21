using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace PhotoBooth.BL.Queries
{
    class AvailableRentalItems : QueryBase<RentalItem, RentalItemModel>
    {
        private DateTime OrderSince;
        private DateTime OrderTill;
        private RentalItemType? RentalItemType;

        public AvailableRentalItems(IUnitOfWorkProvider unitOfWorkProvider, DateTime OrderSince, DateTime OrderTill,
            RentalItemType? rentalItemType=null) : base(unitOfWorkProvider)
        {
            this.OrderSince = OrderSince;
            this.OrderTill = OrderTill;
            RentalItemType = rentalItemType;
        }

        protected override IQueryable<RentalItemModel> GetQueryable()
        {
            var rentedItems = Context.Orders
                .Where(x => ((OrderSince >= x.RentalSince && OrderSince <= x.RentalTill) || (OrderTill >= x.RentalSince && OrderTill <= x.RentalTill) || (OrderSince <= x.RentalSince && OrderTill >= x.RentalTill)))
                .SelectMany(x => x.RentalItems).Select(t=>t.Item.Id).ToList();
            var availableItems = Context.RentalItems.Where(t => !rentedItems.Contains(t.Id));

            if (RentalItemType!=null)
            {
                availableItems = availableItems.Where(t => t.Type == RentalItemType);
            }
            return availableItems
                .ProjectTo<RentalItemModel>(MapConfig);
        }
    }
}
