using System;
using PhotoBooth.BL.Models.Address;

namespace PhotoBooth.BL.Facades
{
    public class OrderMatadata
    {
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
        public AddressModel Address { get; set; }
    }
}