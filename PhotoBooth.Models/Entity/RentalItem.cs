namespace PhotoBooth.Models
{
    public class RentalItem : ItemBase
    {
        public RentalItemType Type { get; set; }
        public decimal PricePerHour { get; set; }
    }
}