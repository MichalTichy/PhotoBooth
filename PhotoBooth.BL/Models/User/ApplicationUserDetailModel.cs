using PhotoBooth.BL.Models.Address;

namespace PhotoBooth.BL.Models.User
{
    public class ApplicationUserDetailModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public AddressModel CustomerAddress { get; set; }
    }
}