namespace PhotoBooth.Models
{
    public class Customer : EntityBase
    {
        public string FirstName { get; set; }
        public  string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public Address CustomerAddress { get; set; }

        //TODO
        //public AppUser User {get; set;}
    }
}