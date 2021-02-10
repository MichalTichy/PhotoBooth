using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoBooth.Mocks
{
    public class OrderFacadeMock : IOrderFacade
    {
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
            return GenerateOrderListModels();
        }

        public ICollection<OrderListModel> GetOrdersByUser(string username, bool includeDeleted = false)
        {
            return GenerateOrderListModels();
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            return GenerateOrderSummary();
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            return GenerateOrderSummary();
        }

        public void ConfirmOrder(Guid orderId)
        {
        }

        public void CancelOrder(Guid orderId)
        {
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
                    Id = Guid.Parse("9d75bbc6-206d-422d-a022-0f34719fc3fd"),
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