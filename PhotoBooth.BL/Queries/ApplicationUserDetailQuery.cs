using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.User;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class ApplicationUserListModelQuery : QueryBase<ApplicationUser, ApplicationUserDetailModel>
    {
        public ApplicationUserListModelQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<ApplicationUserDetailModel> GetQueryable()
        {
            return this.Context.Users
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ProjectTo<ApplicationUserDetailModel>(MapConfig);
        }
    }
}