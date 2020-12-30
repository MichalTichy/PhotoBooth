using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                uow.Save();
            }
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            throw new NotImplementedException("I don't know how to implement function");// TODO I'm not sure what to do here 
        }

        public void ConfirmOrder(Guid orderId)
        {
            throw new NotImplementedException("I don't know how to implement function");// TODO I'm not sure what to do here 
        }

        public ICollection<OrderListModel> GetAllOrders()
        {
            return new OrderListQuery(provider).ExecuteAsync();
        }

        public ICollection<OrderListModel> GetOrdersByUser(Guid userId)
        {
            throw new NotImplementedException("Team consult needed");// TODO I'm not sure what to do here 
            //var ordersQuery = new OrderListQuery(provider);
            //ordersQuery.Where(x => x.Customer.Id == userId);
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            var query = new OrderSummaryQuery(provider);
            query.Where(x => x.Id == id);
            var order = query.ExecuteAsync().First();
            return order;
        }

        public OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            //TODO OrderMetadata countOfHours
            return new OrderSummaryModel() { 
                RentalItems = rentalItems
                , OrderItems = products
                , RentalSince = orderMatadata.Since
                , RentalTill = orderMatadata.Till
                , LocationAddress = orderMatadata.Address
                , Customer = orderMatadata.User};
        }

        public OrderSummaryModel SubmitOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            Order model;
            using (var uow = provider.GetUinOfWork())
            {
                var rentals = rentalItems.Select(x => uow.GetRepo<RentalItem>().Get(y => y.Id == x.Id).First());
                var orders = products.Select(x => uow.GetRepo<Product>().Get(y => y.Id == x.Id).First());
                //  TODO applicatoin user problems 
                //  var user = uow.GetRepo<ApplicationUser>().Get(x => x.Id == orderMatadata.User.Id).First();
                model = new Order()
                {
                    RentalItems = rentals.ToList()
                    , OrderItems = orders.ToList()
                    , RentalSince = orderMatadata.Since
                    , RentalTill = orderMatadata.Till
                    , LocationAddress = new Address() {
                        Id = new Guid()
                        , City = orderMatadata.Address.City
                        , PostalCode = orderMatadata.Address.PostalCode
                        , Street = orderMatadata.Address.Street
                        , BuildingNumber = orderMatadata.Address.BuildingNumber }
                    , Customer = new ApplicationUser("hektor")
                };
                uow.GetRepo<Order>().Update(model);
                uow.Save();
            }
            return PrepareOrder(rentalItems, products, orderMatadata);
        }

    }
}
