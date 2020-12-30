namespace PhotoBooth.DAL.Entity
{
    public class RentalItem : ItemBase
    {
        public RentalItemType Type { get; set; }
        public double PricePerHour { get; set; }
    }
}