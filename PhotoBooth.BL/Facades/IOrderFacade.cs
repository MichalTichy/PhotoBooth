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
        OrderSummaryModel SubmitOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata);
        ICollection<OrderListModel> GetAllOrders();
        ICollection<OrderListModel> GetOrdersByUser(Guid userId);
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