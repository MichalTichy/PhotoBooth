using System;

namespace PhotoBooth.BL.Models.Order
{
    public class OrderListModel : ModelBase
    {
        public DateTime Created { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public string CustomerFullName { get; set; }
        public string Address { get; set; }
        public double FinalPrice { get; set; }
    }
}