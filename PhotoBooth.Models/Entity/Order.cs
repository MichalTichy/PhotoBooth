using System;
using System.Collections.Generic;

namespace PhotoBooth.Models
{
    public class Order : EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public Address LocationAddress { get; set; }
        public Customer Customer { get; set; }
        public ICollection<RentalItem> RentalItems { get; set; }
        public ICollection<Product> OrderItems { get; set; }
        public string BannerUrl { get; set; }
    }
}