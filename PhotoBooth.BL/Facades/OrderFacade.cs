using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Models.User;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    public class OrderFacade : FacadeBase<Order>, IOrderFacade
    {
        private readonly ICatalogFacade _catalogFacade;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly UserFacade _userFacade;

        public OrderFacade(BaseRepository<Order> repository, ICatalogFacade catalogFacade, IUnitOfWorkProvider uow, IDateTimeProvider dateTimeProvider, UserFacade userFacade) : base(repository, uow)
        {
            _catalogFacade = catalogFacade;
            this.dateTimeProvider = dateTimeProvider;
            _userFacade = userFacade;
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(orderId);
                order.CancellationDate = dateTimeProvider.Now;
                foreach (var item in order.OrderItems)
                {
                    item.Item.AmountLeft++;
                    uow.Context.Products.Update(item.Item);
                }
                await uow.CommitAsync();
            }
        }

        public async Task<OrderSummaryModel> ChangeOrderPriceAsync(Guid id, double newPrice)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(id);
                order.FinalPrice = newPrice;
                _repository.Update(order);
                uow.Commit();
            }
            return await GetOrderSummaryAsync(id);
        }

        public void ConfirmOrder(Guid orderId)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(orderId);
                order.ConfirmationDate = dateTimeProvider.Now;
                _repository.Update(order);
                uow.Commit();
            }
        }

        public async Task<ICollection<OrderListModel>> GetAllOrdersAsync(bool includeDeleted = false)
        {
            using (UnitOfWorkFactory.Create())
            {
                return await new OrderListQuery(UnitOfWorkFactory, includeDeleted).ExecuteAsync();
            }
        }

        public async Task<ICollection<OrderListModel>> GetOrdersByUserAsync(string username, bool includeDeleted = false)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new OrderListQuery(UnitOfWorkFactory, includeDeleted);
                query.IncludeOnlyOrdersBySpecificUser(username);
                return await query.ExecuteAsync();
            }
        }

        public async Task<OrderSummaryModel> GetOrderSummaryAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                using var uow = UnitOfWorkFactory.Create();
                var context = (uow as EntityFrameworkUnitOfWork<PhotoBoothContext>)?.Context;
                var order = context.Orders.Include(t => t.OrderItems).ThenInclude(t => t.Item).Include(o => o.RentalItems)
                    .ThenInclude(s => s.Item).FirstOrDefault(t => t.Id == id);

                //sorry for a shitcode :(
                var mapperConfiguration = createMapper();
                var mapper = new Mapper(mapperConfiguration);
                var orderSummaryModel = mapper.Map<OrderSummaryModel>(order);
                return orderSummaryModel;
            });
        }

        public async Task<OrderSummaryModel> PrepareOrderAsync(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            var rentalTill = orderMatadata.Since.AddHours(orderMatadata.CountOfHours);
            return await Task.Run(() =>
            {
                return new OrderSummaryModel()
                {
                    RentalItems = rentalItems,
                    OrderItems = products,
                    RentalSince = orderMatadata.Since,
                    RentalTill = rentalTill,
                    LocationAddress = orderMatadata.Address,
                    Customer = orderMatadata.User,
                    Created = dateTimeProvider.Now,
                    FinalPrice = GetFinalPrice(rentalItems, products, orderMatadata.CountOfHours)
                };
            });
        }

        private double GetFinalPrice(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, int countOfHours)
        {
            return rentalItems.Sum(t => t.PricePerHour) * countOfHours + products.Sum(t => t.Price);
        }

        public async Task<OrderSummaryModel> SubmitOrderAsync(ICollection<RentalItemModel> rentalItems,
            ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var mapperConfiguration = createMapper();
                var orderId = Guid.NewGuid();
                var user = await _userFacade.GetUserByUsername(orderMatadata.User.Email);
                var model = new Order()
                {
                    Id = orderId,
                    RentalItems = rentalItems?.AsQueryable().ProjectTo<RentalItem>(mapperConfiguration).Select(t => new OrderRentalItem() { ItemId = t.Id, OrderId = orderId }).ToList()
                    ,
                    OrderItems = products?.AsQueryable().ProjectTo<Product>(mapperConfiguration).Select(t => new OrderProduct() { ItemId = t.Id, OrderId = orderId }).ToList()
                    ,
                    RentalSince = orderMatadata.Since
                    ,
                    RentalTill = orderMatadata.Since.AddHours(orderMatadata.CountOfHours)
                    ,
                    LocationAddress = new Address()
                    {
                        Id = Guid.NewGuid()
                        ,
                        City = orderMatadata.Address.City
                        ,
                        PostalCode = orderMatadata.Address.PostalCode
                        ,
                        Street = orderMatadata.Address.Street
                        ,
                        BuildingNumber = orderMatadata.Address.BuildingNumber
                    }
                    ,
                    CustomerId = user.Id,
                    Created = dateTimeProvider.Now,
                    FinalPrice = GetFinalPrice(rentalItems, products, orderMatadata.CountOfHours)
                };

                _repository.Insert(model);

                var context = (uow as EntityFrameworkUnitOfWork<PhotoBoothContext>)?.Context;
                if (context != null && products != null)
                {
                    foreach (var product in products)
                    {
                        var productFromDb = await context.Products.FindAsync(product.Id);
                        productFromDb.AmountLeft--;
                    }
                }
                await uow.CommitAsync();
                return await GetOrderSummaryAsync(model.Id);
            }
        }

        private static MapperConfiguration createMapper()
        {
            return new MapperConfiguration(expression =>
            {
                expression.CreateMap<ApplicationUser, ApplicationUserListModel>();
                expression.CreateMap<Order, OrderSummaryModel>();
                expression.CreateMap<RentalItemModel, RentalItem>();
                expression.CreateMap<ProductModel, Product>();
                expression.CreateMap<RentalItemModel, RentalItem>();
                expression.CreateMap<Address, AddressModel>();
                expression.CreateMap<OrderRentalItem, RentalItemModel>().ConvertUsing((i, itemModel, context) => new RentalItemModel()
                {
                    Id = i.Item.Id,
                    DescriptionHtml = i.Item.DescriptionHtml,
                    Name = i.Item.Name,
                    PictureUrl = i.Item.PictureUrl,
                    PricePerHour = i.Item.PricePerHour,
                    Type = i.Item.Type
                });
                expression.CreateMap<OrderProduct, ProductModel>().ConvertUsing((i, itemModel, context) => new ProductModel()
                {
                    Id = i.Item.Id,
                    Name = i.Item.Name,
                    PictureUrl = i.Item.PictureUrl,
                    AmountLeft = i.Item.AmountLeft,
                    DescriptionHtml = i.Item.DescriptionHtml,
                    Price = i.Item.Price
                });
            });
        }
    }
}