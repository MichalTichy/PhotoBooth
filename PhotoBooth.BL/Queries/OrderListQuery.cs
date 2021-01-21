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
        private string username;

        public OrderListQuery(IUnitOfWorkProvider unitOfWorkProvider,bool includeDeleted) : base(unitOfWorkProvider)
        {
            _includeDeleted = includeDeleted;
        }

        public void IncludeOnlyOrdersBySpecificUser(string name)
        {
            this.username = name;
        }

        protected override void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, OrderListModel>().ForMember(model => model.IsCanceled,expression => expression.MapFrom(order => order.CancellationDate.HasValue));
            cfg.CreateMap<Order, OrderListModel>().ForMember(model => model.IsConfirmed,expression => expression.MapFrom(order => order.ConfirmationDate.HasValue));
        }

        protected override IQueryable<OrderListModel> GetQueryable()
        {
            var orders = this.Context.Orders.Include(t=>t.Customer).AsQueryable();

            if (!_includeDeleted)
            {
                orders = orders.Where(t => t.CancellationDate != null);
            }

            if (username!=null)
            {
                orders = orders.Where(t => t.Customer.UserName == username.ToString());
            }

            return orders
                    .OrderBy(x => x.ConfirmationDate)
                .ProjectTo<OrderListModel>(MapConfig);
        }
    }
}
