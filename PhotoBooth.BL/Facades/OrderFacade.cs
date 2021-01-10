using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public OrderFacade(BaseRepository<Order> repository, ICatalogFacade catalogFacade, IUnitOfWorkProvider uow, IDateTimeProvider dateTimeProvider) : base(repository, uow)
        {
            _catalogFacade = catalogFacade;
            this.dateTimeProvider = dateTimeProvider;
        }
        public void CancelOrder(Guid orderId)
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                //TODO: order has cancellation date which should be set (and respected during queries) instead of deleting it.
                _repository.Delete(orderId);
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

            //var availableEmployes =_catalogFacade.GetAvailableRentalItems(orderMatadata.Since,rentalTill,RentalItemType.Employe);
            //rentalItems.Add(availableEmployes.First());
            return new OrderSummaryModel()
            {
                RentalItems = rentalItems,
                OrderItems = products,
                RentalSince = orderMatadata.Since,
                RentalTill = rentalTill,
                LocationAddress = orderMatadata.Address,
                Customer = orderMatadata.User,
                Created = dateTimeProvider.Now,
                FinalPrice = rentalItems.Sum(t=>t.PricePerHour)*orderMatadata.CountOfHours + products.Sum(t=>t.Price)
            };
        }

        public OrderSummaryModel SubmitOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMatadata)
        {
            throw new NotImplementedException();
            ////TODO TEST
            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var mapperConfiguration = new MapperConfiguration(expression =>
            //    {
            //        expression.CreateMap<RentalItemModel,RentalItem>();
            //        expression.CreateMap<ProductModel, Product>();
            //        expression.CreateMap<RentalItemModel,RentalItem>();
            //    });
            //    var model = new Order()
            //    {
            //        RentalItems = rentalItems.AsQueryable().ProjectTo<RentalItem>(mapperConfiguration).ToList()
            //        ,
            //        OrderItems = products.AsQueryable().ProjectTo<Product>(mapperConfiguration).ToList()
            //        ,
            //        RentalSince = orderMatadata.Since
            //        ,
            //        RentalTill = orderMatadata.Till
            //        ,
            //        LocationAddress = new Address()
            //        {
            //            Id = new Guid()
            //            ,
            //            City = orderMatadata.Address.City
            //            ,
            //            PostalCode = orderMatadata.Address.PostalCode
            //            ,
            //            Street = orderMatadata.Address.Street
            //            ,
            //            BuildingNumber = orderMatadata.Address.BuildingNumber
            //        }
            //        ,
            //        Customer = new ApplicationUser("hektor")
            //    };
            //    _repository.Insert(model);
            //    uow.Commit();
            //}
            //return PrepareOrder(rentalItems, products, orderMatadata);
        }

    }
}