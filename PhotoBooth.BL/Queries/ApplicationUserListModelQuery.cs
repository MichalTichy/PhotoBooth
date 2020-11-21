using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.User;

namespace PhotoBooth.BL.Queries
{
    public class ApplicationUserLisstModelQuery : QueryBase<ApplicationUser, ApplicationUserListModel>
    {
        public ApplicationUserLisstModelQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<ApplicationUserListModel> GetQueryable()
        {
            return this.Context.Users
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ProjectTo<ApplicationUserListModel>(MapConfig);
        }
    }
}
