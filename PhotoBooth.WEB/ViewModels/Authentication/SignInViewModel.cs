using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL.Facades;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

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