using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.Models.User
{
    public class ApplicationUserListModel : ModelBase
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}