using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.Item.RentalItem;
using Riganti.Utils.Infrastructure.Core;
using AutoMapper.QueryableExtensions;

namespace PhotoBooth.BL.Queries
{
    public class RentalItemsQuery : QueryBase<RentalItem, RentalItemModel>
    {
        public RentalItemsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<RentalItemModel> GetQueryable()
        {
            return this.Context.Products
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ProjectTo<RentalItemModel>(MapConfig);
        }

    }
}
