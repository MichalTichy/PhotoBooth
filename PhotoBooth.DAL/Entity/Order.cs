using System;
using System.Collections.Generic;

namespace PhotoBooth.DAL.Entity
{
    public class Order : EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public Address LocationAddress { get; set; }
        public ApplicationUser Customer { get; set; }
        public ICollection<RentalItem> RentalItems { get; set; }
        public ICollection<Product> OrderItems { get; set; }
        public string BannerUrl { get; set; }
        public string FinalPrice { get; set; }
    }
}