using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PhotoBooth.Mocks
{
    public class ProductFacadeMock : IProductFacade
    {
        private IList<ProductModel> _products = GenerateProducts();

        public static IList<ProductModel> GenerateProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("6f2eddce-d4b1-4831-a485-c15f9764c656"),
                    DescriptionHtml = "Product description <b> test </b>",
                    Name = "Product 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 130
                },

                new ProductModel()
                {
                    Id = Guid.Parse("f7c0490f-c733-47b6-80ba-6f73f54d9ca1"),
                    DescriptionHtml = "Product 2 description <b> test </b>",
                    Name = "Product 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 10
                },

                new ProductModel()
                {
                    Id = Guid.Parse("d46d25c7-1ba5-4afc-82a4-9924e2321547"),
                    DescriptionHtml = "Product 3 description <b> test </b>",
                    Name = "Product 3",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 133
                }
            };
        }

        public Task<ProductModel> AddProductAsync(ProductModel product)
        {
            return Task.Run(() =>
            {
                if (_products.Any(p => p.Id == product.Id) || (product.Id != Guid.Empty))
                {
                    return product;
                }
                product.Id = Guid.NewGuid();
                _products.Add(product);
                return product;
            });
        }

        public Task<bool> DeleteProductAsync(Guid id)
        {
            return Task.Run(() =>
            {
                var res = _products.SingleOrDefault(p => p.Id == id);
                _products.Remove(res);
                return !(res == null);
            });
        }

        public Task<bool> EditProductAsync(ProductModel product)
        {
            return Task.Run(() =>
            {
                var old = _products.SingleOrDefault(p => p.Id == product.Id);
                if(old == null)
                {
                    return false;
                }
                _products.Remove(old);
                _products.Add(product);
                return true;
            });
        }

        public Task<IList<ProductModel>> GetAllProductsAsync()
        {
            return Task.Run(() => _products);
        }

        public Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            return Task.Run(() => _products.SingleOrDefault(a => a.Id == id));
        }
    }
}
