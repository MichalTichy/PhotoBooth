namespace PhotoBooth.DAL.Entity
{
    public class RentalItem : ItemBase
    {
        public RentalItemType Type { get; set; }
        public decimal PricePerHour { get; set; }
    }
}