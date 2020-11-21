using Riganti.Utils.Infrastructure.Core;
using System.Linq;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PhotoBooth.BL.Queries
{
    public class OrderListQuery : QueryBase<Order, OrderListModel>
    {
        public OrderListQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<OrderListModel> GetQueryable()
        {
            return this.Context.Orders
                .OrderBy(x => x.ConfirmationDate)
                .ProjectTo<OrderListModel>(MapConfig);
        }
    }
}
