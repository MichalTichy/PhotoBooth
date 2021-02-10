using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    internal class OrderProductsQuery : QueryBase<Order, OrderProductsModel>
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