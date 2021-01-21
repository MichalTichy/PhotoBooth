using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserFacade(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAdminAccount()
        {
            var admin = await GetUserByUsername("admin");
            if (admin!=null)
                return;

            var userInfo = await RegisterAsync("admin", "Password1*");
            admin = await GetUserByUsername("admin");
            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.UpdateAsync(admin);
        }

        public async Task<ClaimsIdentity> GetIdentityByUsername(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return CreateIdentity(user);
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
        static Random rnd = new Random();
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
            //TODO send user the temporary password
            var userEntity=new ApplicationUser(user.Email)
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            var result =  await userManager.CreateAsync(userEntity, password);
            
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