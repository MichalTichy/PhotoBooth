using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    class OrderProductsQuery : QueryBase<Order, OrderProductsModel>
    {
        public OrderProductsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<OrderProductsModel> GetQueryable()
        {
            return this.Context.Orders
                .OrderBy(x => x.ConfirmationDate)
                .ProjectTo<OrderProductsModel>(MapConfig);
        }
    }
}
