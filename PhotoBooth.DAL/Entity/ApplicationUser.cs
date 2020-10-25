using Microsoft.AspNetCore.Identity;

namespace PhotoBooth.DAL.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName) : base(userName)
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Address CustomerAddress { get; set; }
    }
}