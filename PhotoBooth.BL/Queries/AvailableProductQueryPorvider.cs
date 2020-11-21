using PhotoBooth.BL.Models.Item.Product;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    class AvailableProductQueryPorvider : ProductsQuery
    {
        public AvailableProductQueryPorvider(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        public IQueryable<ProductModel> GetData()
        {
            return base.GetQueryable().Where(x => x.AmountLeft > 0);
        }
    }
}
