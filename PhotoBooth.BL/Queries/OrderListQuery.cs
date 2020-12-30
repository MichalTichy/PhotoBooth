using System;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace PhotoBooth.BL.Queries
{
    public class OrderListQuery : QueryBase<Order, OrderListModel>
    {
        private readonly bool _includeDeleted;
        private Guid? UserId;
        public OrderListQuery(IUnitOfWorkProvider unitOfWorkProvider,bool includeDeleted) : base(unitOfWorkProvider)
        {
            _includeDeleted = includeDeleted;
        }

        public void IncludeOnlyOrdersBySpecificUser(Guid id)
        {
            UserId = id;
        }

        protected override IQueryable<OrderListModel> GetQueryable()
        {
            var orders = this.Context.Orders.Include(t=>t.Customer).AsQueryable();

            if (!_includeDeleted)
            {
                orders = orders.Where(t => t.CancellationDate != null);
            }

            if (UserId!=null)
            {
                orders = orders.Where(t => t.Customer.Id == UserId.ToString());
            }

            return orders
                    .OrderBy(x => x.ConfirmationDate)
                .ProjectTo<OrderListModel>(MapConfig);
        }
    }
}
