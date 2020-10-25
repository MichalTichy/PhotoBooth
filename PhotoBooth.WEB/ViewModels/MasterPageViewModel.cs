using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Identity;

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
