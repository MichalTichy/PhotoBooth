using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL.Models.User;
using PhotoBooth.DAL.Entity;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    namespace PhotoBooth.BL.Facades
    {
    }

    public class UserFacade
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserFacade(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAdminAccount()
        {
            var admin = await GetUserByUsername("admin@smileshoot.sk");
            if (admin != null)
                return;

            var adminUser = new ApplicationUser("admin@smileshoot.sk")
            {
                Email = "admin@smileshoot.sk",
                FirstName = "Administrator",
                LastName = "SmileShoot",
                PhoneNumber = "+4210910512175"
            };
            var userInfo = await userManager.CreateAsync(adminUser, "Password1*");
            admin = await GetUserByUsername("admin@smileshoot.sk");
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.UpdateAsync(admin);
        }

        public async Task<ClaimsIdentity> GetIdentityByUsername(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return await CreateIdentityAsync(user);
        }

        public async Task<ApplicationUser> GetUserByUsername(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<ClaimsIdentity> SignInAsync(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await userManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    return await CreateIdentityAsync(user);
                }
            }
            return null;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser(email);
            return await userManager.CreateAsync(user, password);
        }

        private static Random rnd = new Random();

        private static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
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

            //EmailFacade.SendEmail(user.Email, password);

            Console.WriteLine("new user: " + user.Email);
            Console.WriteLine("new password: " + password);
            var userEntity = new ApplicationUser(user.Email)
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            var result = await userManager.CreateAsync(userEntity, password);

            return result;
        }

        private async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user)
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                }, "Cookie");
            if (await userManager.IsInRoleAsync(user, "admin"))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            }
            return identity;
        }
    }
}