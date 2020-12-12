using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Models.User;

namespace PhotoBooth.Mocks
{
    public class OrderFacadeMock : IOrderFacade
    {

        private List<OrderSummaryModel> _orderSummaries = new List<OrderSummaryModel>() { GenerateOrderSummary() };

        public OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems,
            ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            return GenerateOrderSummary(rentalItems, products, orderMatadata); 
        }

        public Task<OrderSummaryModel> SubmitOrder(ICollection<RentalItemModel> rentalItems,
            ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            return Task.FromResult(GenerateOrderSummary(rentalItems, products, orderMatadata));
        }

        private static OrderSummaryModel GenerateOrderSummary(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            return new OrderSummaryModel()
            {
                Id = Guid.NewGuid(),
                RentalItems = rentalItems,
                Created = DateTime.Now,
                BannerUrl = @"https://picsum.photos/200",
                Customer = orderMatadata?.User,
                FinalPrice = 1234,
                LocationAddress = orderMatadata?.Address,
                OrderItems = products,
                RentalSince = orderMatadata.Since,
                RentalTill = orderMatadata.Since.AddHours(orderMatadata.CountOfHours)
            };
        }

        public ICollection<OrderListModel> GetAllOrders(bool includeDeleted = false)
        {
            return _orderSummaries.Select(o => new OrderListModel()
            {
                Address = o.LocationAddress.ToString(),
                Id = o.Id,
                RentalSince = o.RentalSince,
                RentalTill = o.RentalTill,
                Created = new DateTime(2020, 10, 3, 16, 0, 0),
                IsConfirmed = o.ConfirmationDate == null ? false : true,
                FinalPrice = o.FinalPrice,
                CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName,
            }).ToList();
        }


        public ICollection<OrderListModel> GetOrdersByUser(string username, bool includeDeleted = false)
        {
            if (userId == Guid.Parse("df88b24e-84c0-4758-8fdd-61b176563b23"))
            {
                return new List<OrderListModel>(){ GenerateOrderListModels().First() };
            }
            return new List<OrderListModel>();
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            if (id == Guid.Parse("119567d0-fab4-4e40-9c57-bfa9d8e80732"))
            {
                return GenerateOrderSummary();
            }
            return null;
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            return GenerateOrderSummary();
        }



        public bool ConfirmOrder(Guid orderId)
        {
            for (int i = 0; i < _orderSummaries.Count(); i++)
            {
                if (_orderSummaries[i].Id == orderId)
                {
                    _orderSummaries[i].ConfirmationDate = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        //return ordersummarymodel in case it's in _orderSummaries list, null otherwise
        public OrderSummaryModel GetOrderById(Guid id)
        {
            foreach (var order in _orderSummaries)
            {
                if (order.Id == id)
                {
                    return order;
                }
            }
            return null;
        }

        public bool UpdateOrder(OrderSummaryModel order)
        {
            for (int i = 0; i < _orderSummaries.Count(); i++)
            {
                if (_orderSummaries[i].Id == order.Id)
                {
                    _orderSummaries.Remove(_orderSummaries[i]);
                    _orderSummaries.Add(order);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteOrder(Guid orderId)
        {
            for (int i = 0; i < _orderSummaries.Count(); i++)
            {
                if (_orderSummaries[i].Id == orderId)
                {
                    _orderSummaries.Remove(_orderSummaries[i]);
                    return true;
                }
            }
            return false;
        }

        public OrderSummaryModel CreateOrder(OrderSummaryModel order)
        {
            //cannot create order with guid
            if (order.Id != Guid.Empty)
            {
                return order;
            }
            order.Id = Guid.NewGuid();
            foreach (var o in _orderSummaries)
            {
                if (o.Id == order.Id)
                {
                    throw new InvalidOperationException("Order with this id already exists.");
                }
            }
            _orderSummaries.Add(order);
            return order;
        }

        public bool CancelOrder(Guid orderId)
        {
            for (int i = 0; i < _orderSummaries.Count(); i++)
            {
                if (_orderSummaries[i].Id == orderId)
                {
                    _orderSummaries[i].ConfirmationDate = null;
                    return true;
                }
            }
            return false;
        }

        public static OrderSummaryModel GenerateOrderSummary()
        {
            return new OrderSummaryModel()
            {
                Id = Guid.Parse("119567d0-fab4-4e40-9c57-bfa9d8e80732"),
                BannerUrl = @"https://picsum.photos/200",
                RentalItems = CatalogFacadeMock.GenerateRentalItems(),
                OrderItems = CatalogFacadeMock.GenerateProducts(),
                RentalSince = new DateTime(2020, 12, 3, 16, 0, 0),
                RentalTill = new DateTime(2020, 12, 3, 23, 0, 0),
                Created = new DateTime(2020, 10, 3, 16, 0, 0),
                FinalPrice = 1234,
                ConfirmationDate = new DateTime(2020, 10, 5, 16, 0, 0),
                Customer = new ApplicationUserListModel()
                {
                    Id = Guid.Parse("df88b24e-84c0-4758-8fdd-61b176563b23"),
                    FirstName = "Jana",
                    LastName = "Macková"
                },
                LocationAddress = new AddressModel()
                {
                    Id = Guid.Parse("6bf72cde-681e-40bb-88e8-f859c8cb6321"),
                    BuildingNumber = "1592",
                    City = "Rychnov nad Knežnou",
                    PostalCode = "516 01",
                    Street = "Obránců míru"
                }
            };
        }
        public static ICollection<OrderListModel> GenerateOrderListModels()
        {
            return new List<OrderListModel>()
            {
                new OrderListModel()
                {
                    Address = "Obránců míru 1592 Rychnov nad Knežnou 516 01",
                    Id = Guid.Parse("119567d0-fab4-4e40-9c57-bfa9d8e80732"),
                    RentalSince = new DateTime(2020, 12, 3, 16, 0, 0),
                    RentalTill = new DateTime(2020, 12, 3, 23, 0, 0),
                    Created = new DateTime(2020, 10, 3, 16, 0, 0),
                    IsConfirmed = true,
                    FinalPrice = 1234,
                    CustomerFullName = "Jana Macková",
                },

                new OrderListModel()
                {
                    Address = "Hájecká 1800 Malíkovice 273 77",
                    Id = Guid.Parse("9d75bbc6-206d-422d-a022-0f34719fc3fd"),
                    RentalSince = new DateTime(2020, 12, 8, 16, 0, 0),
                    RentalTill = new DateTime(2020, 12, 8, 23, 0, 0),
                    Created = new DateTime(2020, 11, 3, 16, 0, 0),
                    IsConfirmed = true,
                    FinalPrice = 4321,
                    CustomerFullName = "Marta Jurčíková",
                }
            };
        }
    }
}