using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class AvailableProductsQuery : QueryBase<Product, ProductModel>
    {
        public AvailableProductsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<ProductModel> GetQueryable()
        {
            return this.Context.Products.Where(t => t.AmountLeft > 0)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ProjectTo<ProductModel>(MapConfig);
        }
    }
}