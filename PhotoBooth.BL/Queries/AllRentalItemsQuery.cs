using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    public class AllRentalItemsQuery : QueryBase<RentalItem, RentalItemModel>
    {
        public AllRentalItemsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<RentalItemModel> GetQueryable()
        {
            return this.Context.RentalItems.OrderBy(x => x.Name)
                .ThenBy(x => x.Type)
                .ProjectTo<RentalItemModel>(MapConfig);
        }
    }
}
