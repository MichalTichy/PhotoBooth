namespace PhotoBooth.Models
{
    public class RentalItemDTO : ItemBase
    {
        public RentalItemType Type { get; set; }
        public decimal PricePerHour { get; set; }
    }


}