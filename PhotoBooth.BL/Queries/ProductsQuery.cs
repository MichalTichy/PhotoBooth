using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class ProductsQuery : QueryBase<Product, ProductModel>
    {
        public ProductsQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
