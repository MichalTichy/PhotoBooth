using System;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.DAL.Entity;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PhotoBooth.BL.Models.User;

namespace PhotoBooth.BL.Facades
{
    namespace PhotoBooth.BL.Facades
    {
    }

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

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser(email);
            return await userManager.CreateAsync(user, password);
        }
        private static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (2 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            res.Append('@');
            res.Append('8');
            return res.ToString();
        }

        public async Task<IdentityResult> RegisterSendTemporaryPasswordAsync(ApplicationUserListModel user)
        {
            var password = CreatePassword(15);
            //TODO send user the temporary password
            var userEntity=new ApplicationUser(user.Email)
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            var result =  await userManager.CreateAsync(userEntity, password);
            if (!result.Succeeded)
                return result;
            await SignInAsync(user.Email, password);
            return result;
        }

        private ClaimsIdentity CreateIdentity(ApplicationUser user)
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, "administrator"),
                }, "Cookie");
            return identity;
        }

    }
}