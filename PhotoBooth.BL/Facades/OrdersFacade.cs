using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Queries;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;

namespace PhotoBooth.BL.Facades
{
    public class OrdersFacade : FacadeBase<Order>, IOrderFacade
    {
        public OrdersFacade(BaseRepository<Order> repository, IUnitOfWorkProvider uow) : base(repository, uow)
        {

        }
     
        public bool CancelOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            throw new NotImplementedException();
        }

        public bool ConfirmOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public OrderSummaryModel CreateOrder(OrderSummaryModel order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public ICollection<OrderListModel> GetAllOrders()
        {
            using (UnitOfWorkFactory.Create())
            {
                return new OrderListQuery(UnitOfWorkFactory).Execute();
            }
        }

        public OrderSummaryModel GetOrderById(Guid id)
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

        public bool UpdateOrder(OrderSummaryModel order)
        {
            throw new NotImplementedException();
        }
    }
}
