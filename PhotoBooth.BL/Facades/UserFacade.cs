using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.BL.Facades
{
    public class UserFacade
    {
        private readonly UserManager<ApplicationUser> userManager;
		
        public UserFacade(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ClaimsIdentity> SignInAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    return CreateIdentity(user);
                }
            }
            return null;
        }

        public async Task<IdentityResult> RegisterAsync(string userName, string password)
        {
            var user = new ApplicationUser(userName);
            return await userManager.CreateAsync(user, password);
        }

        private ClaimsIdentity CreateIdentity(ApplicationUser user)
        {
            var identity = new ClaimsIdentity(
                new []
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id), 
                    new Claim(ClaimTypes.Role, "administrator"), 
                },"Cookie");
            return identity;
        }

    }
}