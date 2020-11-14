using System;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.User;

namespace PhotoBooth.BL.Facades
{
    public class OrderMatadata
    {
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
        public AddressModel Address { get; set; }
        public ApplicationUserListModel User { get; set; }
    }
}