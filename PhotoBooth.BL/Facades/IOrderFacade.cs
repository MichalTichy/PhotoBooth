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
        Task<OrderSummaryModel> PrepareOrderAsync(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata);

        Task<OrderSummaryModel> SubmitOrderAsync(ICollection<RentalItemModel> rentalItems,
        ICollection<ProductModel> products, OrderMatadata orderMatadata);

        Task<ICollection<OrderListModel>> GetAllOrdersAsync(bool includeDeleted = false);

        Task<ICollection<OrderListModel>> GetOrdersByUserAsync(string username, bool includeDeleted = false);

        Task<OrderSummaryModel> GetOrderSummaryAsync(Guid id);

        Task<OrderSummaryModel> ChangeOrderPriceAsync(Guid id, double newPrice);

        void ConfirmOrder(Guid orderId);

        void CancelOrder(Guid orderId);
    }
}