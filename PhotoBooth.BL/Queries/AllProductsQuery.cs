using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class AllProductsQuery : QueryBase<Product, ProductModel>
    {
        public AllProductsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<ProductModel> GetQueryable()
        {
            return this.Context.Products.OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ProjectTo<ProductModel>(MapConfig);
        }
    }
}