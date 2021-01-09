
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.User;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class ApplicationUserListModelQuery : QueryBase<ApplicationUser, ApplicationUserDetailModel>
    {
        public ApplicationUserListModelQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
