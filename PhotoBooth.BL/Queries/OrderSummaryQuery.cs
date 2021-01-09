
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class OrderSummaryQuery : QueryBase<Order, OrderSummaryModel>
    {
        public OrderSummaryQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
