using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
namespace PhotoBooth.DAL.Entity
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        public ApplicationUser(string userName) : base(userName)
        {
        }
        //TODO need to check with mates
        public ApplicationUser() : base(Guid.NewGuid().ToString())
        {
        }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        
        public Address CustomerAddress { get; set; }
        Guid IEntity.Id { get; set; }
    }
}