using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.User;
using PhotoBooth.BL.ValidationRules;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.Facades
{
    public class OrderMatadata
    {
        [InTheFuture("Vypůjčka nemůže začínat v minulosti!")]
        public DateTime Since { get; set; } = DateTime.Now;

        [Range(2, 5)]
        public int CountOfHours { get; set; } = 2;

        public AddressModel Address { get; set; } = new AddressModel();
        public ApplicationUserListModel User { get; set; }
    }
}