using System;
using System.Collections.Generic;

namespace PhotoBooth.Models
{
    public class OrderDTO : DTOBase
    {
        public DateTime Created { get; set; }
        public DateTime RentalSince { get; set; }
        public DateTime RentalTill { get; set; }
        public AddressDTO LocationAddress { get; set; }
        public CustomerDTO Customer { get; set; }
        public ICollection<RentalItemDTO> RentalItems { get; set; }
        public ICollection<ProductDTO> OrderItems { get; set; }
        public string BannerUrl { get; set; }
    }
}