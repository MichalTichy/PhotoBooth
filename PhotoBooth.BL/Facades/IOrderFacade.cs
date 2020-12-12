using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;

namespace PhotoBooth.BL.Facades
{
    public interface IOrderFacade
    {
        OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata);
        Task<OrderSummaryModel> SubmitOrder(ICollection<RentalItemModel> rentalItems,
        ICollection<ProductModel> products, OrderMatadata orderMatadata);
        ICollection<OrderListModel> GetAllOrders(bool includeDeleted=false);
        ICollection<OrderListModel> GetOrdersByUser(string username, bool includeDeleted = false);
        OrderSummaryModel GetOrderSummary(Guid id);
        OrderSummaryModel ChangeOrderPrice(Guid id,double newPrice);
        bool ConfirmOrder(Guid orderId);
        bool CancelOrder(Guid orderId);
        bool UpdateOrder(OrderSummaryModel order);
        bool DeleteOrder(Guid orderId);
        OrderSummaryModel GetOrderById(Guid id);
        OrderSummaryModel CreateOrder(OrderSummaryModel order);
    }
}