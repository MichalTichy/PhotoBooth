using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;

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

        public ICollection<OrderListModel> GetOrdersByUser(Guid userId,bool includeDeleted = false)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new OrderListQuery(UnitOfWorkFactory, includeDeleted);
                query.IncludeOnlyOrdersBySpecificUser(userId);
                return query.Execute();
            }
        }

        public OrderSummaryModel GetOrderSummary(Guid id)
        {
            var order = _repository.GetById(id);

            //sorry for a shitcode :(
            var mapper = new Mapper(new MapperConfiguration(expression =>
            {
                expression.CreateMap<Order, OrderSummaryModel>();
            }));
            var orderSummaryModel = mapper.Map<OrderSummaryModel>(order);
            return orderSummaryModel;
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
                var mapperConfiguration = new MapperConfiguration(expression =>
                {
                    expression.CreateMap<Order, OrderSummaryModel>();
                    expression.CreateMap<RentalItemModel, RentalItem>();
                    expression.CreateMap<ProductModel, Product>();
                    expression.CreateMap<RentalItemModel, RentalItem>();
                    expression.CreateMap<Address, AddressModel>();
                    expression.CreateMap<OrderRentalItem, RentalItemModel>().ConvertUsing((item, itemModel, context) => context.Mapper.Map<RentalItemModel>(item.Item));
                    expression.CreateMap<OrderProduct, ProductModel>().ConvertUsing((item, itemModel, context) => context.Mapper.Map<ProductModel>(item.Item));
                });
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
                await uow.CommitAsync();

                return new Mapper(mapperConfiguration).Map<OrderSummaryModel>(model);
            }

        }

    }
}