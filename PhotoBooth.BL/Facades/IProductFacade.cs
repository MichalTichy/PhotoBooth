using PhotoBooth.BL.Models.Item.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    public interface IProductFacade
    {
        Task<IList<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task<ProductModel> AddProductAsync(ProductModel product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<bool> EditProductAsync(ProductModel product);
    }
}
