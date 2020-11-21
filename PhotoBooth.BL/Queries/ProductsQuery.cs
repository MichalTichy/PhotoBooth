using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.Item.Product;
using Riganti.Utils.Infrastructure.Core;
using AutoMapper.QueryableExtensions;

namespace PhotoBooth.BL.Queries
{
    public class ProductsQuery : QueryBase<Product, ProductModel>
    {
        public ProductsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<ProductModel> GetQueryable()
        {
            return this.Context.Products
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ProjectTo<ProductModel>(MapConfig);
        }
    }
}
