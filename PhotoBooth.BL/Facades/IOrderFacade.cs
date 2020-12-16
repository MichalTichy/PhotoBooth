﻿using System;
using System.Collections.Generic;
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
        void ConfirmOrder(Guid orderId);
        void CancelOrder(Guid orderId);
        OrderSummaryModel GetOrderById(Guid id);
    }
}