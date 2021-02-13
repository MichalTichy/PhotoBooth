using PhotoBooth.BL.Models;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
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
    public class CatalogFacade : FacadeBase<Product>, ICatalogFacade
    {
        public CatalogFacade(BaseRepository<Product> repository, IUnitOfWorkProvider uow) : base(repository, uow)
        {
        }

        public async Task<bool> AreAllRentalItemsAvailableAsync(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            using (UnitOfWorkFactory.Create())
            {
                var available = await new AvailableRentalItems(UnitOfWorkFactory, since, till).ExecuteAsync();
                return items.All(x => available.Contains(x));
            }
        }

        public async Task CreateProductAsync(ProductModel newProduct)
        {
            Product p = new Product()
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                AmountLeft = newProduct.AmountLeft,
                DescriptionHtml = newProduct.DescriptionHtml,
                PictureUrl = newProduct.PictureUrl,
                Price = newProduct.Price
            };
            using (var uow = UnitOfWorkFactory.Create())
            {
                _repository.Insert(p);
                await uow.CommitAsync();
            }
        }

        public async Task CreateRentalItemAsync(RentalItemModel newItem)
        {
            RentalItem ri = new RentalItem()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                DescriptionHtml = newItem.DescriptionHtml,
                PictureUrl = newItem.PictureUrl,
                PricePerHour = newItem.PricePerHour,
                Type = newItem.Type
            };
            using var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkFactory.Create();
            uow.Context.RentalItems.Add(ri);
            await uow.CommitAsync();
        }

        public async Task DeleteProductAsync(ProductModel product)
        {
            Product p = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                AmountLeft = product.AmountLeft,
                DescriptionHtml = product.DescriptionHtml,
                PictureUrl = product.PictureUrl,
                Price = product.Price
            };
            using (var uow = UnitOfWorkFactory.Create())
            {
                _repository.Delete(p);
                await uow.CommitAsync();
            }
        }

        public async Task DeleteRentalItemAsync(RentalItemModel rentalItem)
        {
            RentalItem ri = new RentalItem()
            {
                Id = rentalItem.Id,
                Name = rentalItem.Name,
                DescriptionHtml = rentalItem.DescriptionHtml,
                PictureUrl = rentalItem.PictureUrl,
                PricePerHour = rentalItem.PricePerHour,
                Type = rentalItem.Type
            };
            using var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkFactory.Create();
            uow.Context.RentalItems.Remove(ri);
            await uow.CommitAsync();
        }

        public async Task<ICollection<ItemPackageDTO>> GetAllPackagesAsync()
        {
            using (UnitOfWorkFactory.Create())
            {
                return await new ItemPackagesQuery(UnitOfWorkFactory).ExecuteAsync();
            }
        }

        public async Task<ICollection<ProductModel>> GetAllProductsAsync()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var query = new AllProductsQuery(UnitOfWorkFactory);
                return await query.ExecuteAsync();
            }
        }

        public async Task<ICollection<RentalItemModel>> GetAllRentalItemsAsync()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var query = new AllRentalItemsQuery(UnitOfWorkFactory);
                return await query.ExecuteAsync();
            }
        }

        public async Task<ICollection<ProductModel>> GetAvailableProductsAsync()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var query = new AvailableProductsQuery(UnitOfWorkFactory);
                return await query.ExecuteAsync();
            }
        }

        public async Task<ICollection<RentalItemModel>> GetAvailableRentalItemsAsync(DateTime since, DateTime till, RentalItemType? type = null)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new AvailableRentalItems(UnitOfWorkFactory, since, till, type);
                return await query.ExecuteAsync();
            }
        }

        public async Task UpdateProductAsync(ProductModel product)
        {
            Product p = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                AmountLeft = product.AmountLeft,
                DescriptionHtml = product.DescriptionHtml,
                PictureUrl = product.PictureUrl,
                Price = product.Price
            };
            using (var uow = UnitOfWorkFactory.Create())
            {
                _repository.Update(p);
                await uow.CommitAsync();
            }
        }

        public async Task UpdateRentalItemAsync(RentalItemModel rentalItem)
        {
            RentalItem ri = new RentalItem()
            {
                Id = rentalItem.Id,
                Name = rentalItem.Name,
                DescriptionHtml = rentalItem.DescriptionHtml,
                PictureUrl = rentalItem.PictureUrl,
                PricePerHour = rentalItem.PricePerHour,
                Type = rentalItem.Type
            };
            using var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkFactory.Create();
            uow.Context.RentalItems.Update(ri);
            await uow.CommitAsync();
        }
    }
}