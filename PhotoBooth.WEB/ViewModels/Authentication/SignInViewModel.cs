using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL;
using PhotoBooth.BL.Facades;

namespace PhotoBooth.WEB.ViewModels.Authentication
{
    public class SignInViewModel : MasterPageViewModel
    {

        private readonly UserFacade _userFacade;

        public SignInViewModel(UserFacade userFacade)
        {
            this._userFacade = userFacade;
        }

        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task SignIn()
        {
            var identity = await _userFacade.SignInAsync(UserName, Password);
            if (identity == null)
            {
                Context.ModelState.Errors.Add(new ViewModelValidationError
                {
                    ErrorMessage = "Neznáme prihlasovacie údaje",
                    PropertyPath = nameof(Password)
                });
                Context.FailOnInvalidModelState();
            }
			else
            {
                var claimsPrincipal = new ClaimsPrincipal(identity);
                var authenticationManager = Context.GetAuthentication();
                await authenticationManager.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);
				Context.RedirectToRoute("Default", allowSpaRedirect: false);
			} 
        }
    }
}
