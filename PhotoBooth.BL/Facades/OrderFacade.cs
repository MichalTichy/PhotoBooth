﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace PhotoBooth.BL.Facades
{
    public class OrderFacade : FacadeBase<Order>, IOrderFacade
    {
        private readonly ICatalogFacade _catalogFacade;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly UserFacade _userFacade;

        public OrderFacade(BaseRepository<Order> repository, ICatalogFacade catalogFacade, IUnitOfWorkProvider uow, IDateTimeProvider dateTimeProvider,UserFacade userFacade) : base(repository, uow)
        {
            _catalogFacade = catalogFacade;
            this.dateTimeProvider = dateTimeProvider;
            _userFacade = userFacade;
        }
        public void CancelOrder(Guid orderId)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(orderId);
                order.CancellationDate = dateTimeProvider.Now;
                uow.Commit();
            }
        }

        public OrderSummaryModel ChangeOrderPrice(Guid id, double newPrice)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(id);
                order.FinalPrice = newPrice;
                _repository.Update(order);
                uow.Commit();
            }
            return GetOrderSummary(id);
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

        public ICollection<OrderListModel> GetAllOrders(bool includeDeleted = false)
        {
            using (UnitOfWorkFactory.Create())
            {
                return new OrderListQuery(UnitOfWorkFactory, includeDeleted).Execute();
            }
        }

        public ICollection<OrderListModel> GetOrdersByUser(string username,bool includeDeleted = false)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new OrderListQuery(UnitOfWorkFactory, includeDeleted);
                query.IncludeOnlyOrdersBySpecificUser(username);
                return query.Execute();
            }
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            using (UnitOfWorkFactory.Create())
            {
                var order = _repository.GetById(id);

                //sorry for a shitcode :(
                var mapperConfiguration = createMapper();
                var mapper= new Mapper(mapperConfiguration);
                var orderSummaryModel = mapper.Map<OrderSummaryModel>(order);
                return orderSummaryModel;
            }
        }

        public OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            var rentalTill = orderMatadata.Since.AddHours(orderMatadata.CountOfHours);

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
        }

        private double GetFinalPrice(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products,int countOfHours )
        {
            return rentalItems.Sum(t=>t.PricePerHour)*countOfHours + products.Sum(t=>t.Price);
        }

        public async Task<OrderSummaryModel> SubmitOrder(ICollection<RentalItemModel> rentalItems,
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
                    RentalItems = rentalItems?.AsQueryable().ProjectTo<RentalItem>(mapperConfiguration).Select(t=>new OrderRentalItem(){ItemId = t.Id,OrderId = orderId}).ToList()
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
                    FinalPrice = GetFinalPrice(rentalItems,products,orderMatadata.CountOfHours)
                };

                _repository.Insert(model);

                var context = (uow as EntityFrameworkUnitOfWork<PhotoBoothContext>)?.Context;
                if (context!=null && products!=null)
                {
                    foreach (var product in products)
                    {
                        var productFromDb = await context.Products.FindAsync(product.Id);
                        productFromDb.AmountLeft--;
                    }
                }
                await uow.CommitAsync();

                return new Mapper(mapperConfiguration).Map<OrderSummaryModel>(model);
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