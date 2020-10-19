namespace PhotoBooth.Models
{
    public class Address : EntityBase
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
    }
}