using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    class OrderProductsQuery : QueryBase<Order, OrderProductsModel>
    {
        public OrderProductsQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
