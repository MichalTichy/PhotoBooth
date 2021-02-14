using System;

namespace PhotoBooth.BL.Models.Order
{
    public class OrderListModel : ModelBase
    {
        public DateTime Created { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCancelled { get; set; }

        public string State
        {
            get
            {
                if (!IsConfirmed && !IsCancelled)
                {
                    return "Cakajuca";
                }
                if (IsCancelled)
                {
                    return "Zrusena";
                }
                if (IsConfirmed)
                {
                    return "Potvrdena";
                }
                return "Neznamy";
            }
        }

        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public string CustomerFullName { get; set; }
        public string Address { get; set; }
        public double FinalPrice { get; set; }
    }
}