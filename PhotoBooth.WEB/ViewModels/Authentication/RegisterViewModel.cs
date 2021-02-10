using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Validation;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL.Facades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoBooth.WEB.ViewModels.Authentication
{
    public class RegisterViewModel : MasterPageViewModel, IValidatableObject
    {
        private readonly UserFacade _userFacade;

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public RegisterViewModel(UserFacade userFacade)
        {
            this._userFacade = userFacade;
        }

        public async Task Register()
        {
            var identityResult = await _userFacade.RegisterAsync(UserName, Password);
            if (identityResult.Succeeded)
            {
                await SignIn();
            }
            else
            {
                var modelErrors = ConvertIdentityErrorsToModelErrors(identityResult);
                Context.ModelState.Errors.AddRange(modelErrors);
                Context.FailOnInvalidModelState();
            }

            Context.RedirectToRoute("Default", allowSpaRedirect: false);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Zadaná hesla se neschodují!", new[] { nameof(ConfirmPassword) });
            }
        }

        private async Task SignIn()
        {
            var claimsIdentity = await _userFacade.SignInAsync(UserName, Password);
            await Context.GetAuthentication().SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        private IEnumerable<ViewModelValidationError> ConvertIdentityErrorsToModelErrors(IdentityResult identityResult)
        {
            return identityResult.Errors.Select(identityError => new ViewModelValidationError
            {
                ErrorMessage = identityError.Description,
                PropertyPath = nameof(UserName)
            });
        }
    }
}