using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.User;

namespace PhotoBooth.BL.Models.Order
{
    public class OrderSummaryModel : ModelBase
    {
        public DateTime Created { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public int CountOfHours => (int)Math.Ceiling((RentalTill - RentalSince).TotalHours);

        public AddressModel LocationAddress { get; set; }
        [Required]
        public ApplicationUserListModel Customer { get; set; }
        public ICollection<RentalItemModel> RentalItems { get; set; }
        public ICollection<ProductModel> OrderItems { get; set; }
        public string BannerUrl { get; set; }
        public double FinalPrice { get; set; }
    }
}