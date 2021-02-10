using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.DAL.Entity
{
    public class Order : EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public virtual Address LocationAddress { get; set; }
        public virtual ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        public virtual ICollection<OrderRentalItem> RentalItems { get; set; }
        public virtual ICollection<OrderProduct> OrderItems { get; set; }

        [MaxLength(100)]
        public string BannerUrl { get; set; }

        public double FinalPrice { get; set; }
    }

    public class OrderRentalItem
    {
        public Guid ItemId { get; set; }
        public virtual RentalItem Item { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

    public class OrderProduct
    {
        public Guid ItemId { get; set; }
        public virtual Product Item { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}