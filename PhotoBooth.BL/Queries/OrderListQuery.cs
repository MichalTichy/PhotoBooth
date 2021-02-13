using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class OrderListQuery : QueryBase<Order, OrderListModel>
    {
        private readonly bool _includeCancelled;
        private string username;

        public OrderListQuery(IUnitOfWorkProvider unitOfWorkProvider, bool includeCancelled) : base(unitOfWorkProvider)
        {
            _includeCancelled = includeCancelled;
        }

        public void IncludeOnlyOrdersBySpecificUser(string name)
        {
            this.username = name;
        }

        protected override void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, OrderListModel>()
                .ForMember(model => model.IsCancelled, expression => expression.MapFrom(order => order.CancellationDate.HasValue))
                .ForMember(model => model.IsConfirmed, expression => expression.MapFrom(order => order.ConfirmationDate.HasValue))
                .ForMember(model => model.Address, expression => expression.MapFrom(order => order.LocationAddress.City));
        }

        protected override IQueryable<OrderListModel> GetQueryable()
        {
            var orders = this.Context.Orders.Include(t => t.Customer).AsQueryable();

            if (!_includeCancelled)
            {
                orders = orders.Where(t => t.CancellationDate != null);
            }

            if (username != null)
            {
                orders = orders.Where(t => t.Customer.UserName == username);
            }

            return orders
                    .OrderBy(x => x.ConfirmationDate)
                .ProjectTo<OrderListModel>(MapConfig);
        }
    }
}