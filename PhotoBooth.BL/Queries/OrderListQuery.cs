using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class OrderListQuery : QueryBase<Order, OrderListModel>
    {
        public OrderListQuery(IUnitOfWorkProvider provider) : base(provider) { }

    }
}
