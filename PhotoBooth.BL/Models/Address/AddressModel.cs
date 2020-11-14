namespace PhotoBooth.BL.Models.Address
{
    public class AddressModel : ModelBase
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
    }
}