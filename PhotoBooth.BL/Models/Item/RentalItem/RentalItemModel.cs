using PhotoBooth.DAL.Entity;

namespace PhotoBooth.BL.Models.Item.RentalItem
{
    public class RentalItemModel : ItemBaseModel
    {
        public RentalItemType Type { get; set; }
        public double PricePerHour { get; set; }
    }
}