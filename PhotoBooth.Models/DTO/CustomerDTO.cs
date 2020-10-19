namespace PhotoBooth.Models
{
    public class CustomerDTO : DTOBase
    {
        public string FirstName { get; set; }
        public  string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }

        public AddressDTO CustomerAddress { get; set; }
    }
}