using System.Collections.Generic;

namespace PhotoBooth.DAL.Entity
{
    public class RentalItem : ItemBase
    {
        public RentalItemType Type { get; set; }
        public decimal PricePerHour { get; set; }
        public List<BorrowTime> BorrowedTime { get; set; }
    }
}