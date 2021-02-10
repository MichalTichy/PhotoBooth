using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    public interface IOrderFacade
    {
        OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata);

        Task<OrderSummaryModel> SubmitOrder(ICollection<RentalItemModel> rentalItems,
        ICollection<ProductModel> products, OrderMatadata orderMatadata);

        ICollection<OrderListModel> GetAllOrders(bool includeDeleted = false);

        ICollection<OrderListModel> GetOrdersByUser(string username, bool includeDeleted = false);

        OrderSummaryModel GetOrderSummary(Guid id);

        OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice);

        void ConfirmOrder(Guid orderId);

        void CancelOrder(Guid orderId);
    }
}