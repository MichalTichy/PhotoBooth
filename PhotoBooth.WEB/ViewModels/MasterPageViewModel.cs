using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PhotoBooth.WEB.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        public async Task SignOut()
        {
            await Context.GetAuthentication().SignOutAsync(IdentityConstants.ApplicationScheme);
            Context.RedirectToRoute("Default", null, false, false);
        }
    }
}