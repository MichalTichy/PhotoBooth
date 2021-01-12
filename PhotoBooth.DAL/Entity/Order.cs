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
        public Address LocationAddress { get; set; }
        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        public ICollection<OrderRentalItem> RentalItems { get; set; }
        public ICollection<OrderProduct> OrderItems { get; set; }
        [MaxLength(100)]
        public string BannerUrl { get; set; }
        public double FinalPrice { get; set; }
    }

    public class OrderRentalItem
    {
        public Guid ItemId { get; set; }
        public RentalItem Item { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
    public class OrderProduct
    {
        public Guid ItemId { get; set; }
        public Product Item { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}