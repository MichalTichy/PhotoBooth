using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.BL.Facades
{
    public class OrderFacade : FacadeBase<Order>, IOrderFacade
    {
        public OrderFacade(UnitOfWorkProvider provider) : base(provider) { }
        public void CancelOrder(Guid orderId)
        {
            using (var uow = provider.GetUinOfWork())
            {
                uow.GetRepo<Order>().Delete(orderId);
                uow.Commit();
            }
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            throw new NotImplementedException();// TODO I'm not sure
        }

        public void ConfirmOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public ICollection<OrderListModel> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public ICollection<OrderListModel> GetOrdersByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            throw new NotImplementedException();
        }

        public OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            throw new NotImplementedException();
        }

        public OrderSummaryModel SubmitOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            throw new NotImplementedException();
        }
    }
}
