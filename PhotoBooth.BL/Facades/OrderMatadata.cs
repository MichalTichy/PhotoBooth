using System;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Models.User;
using PhotoBooth.BL.ValidationRules;

namespace PhotoBooth.BL.Facades
{
    public class OrderMatadata
    {
        [InTheFuture("Vypůjčka nemůže začínat v minulosti!")]
        public DateTime Since { get; set; } = DateTime.Now;
        [AttributeGreaterThan(nameof(Since),ErrorMessage = "Datum konce vypůjčky nemůže být před zažátkem vypůjčky.")]
        public DateTime Till { get; set; } =DateTime.Now.AddHours(1);

        public int CountOfHours => (int) Math.Ceiling((Till - Since).TotalHours);
        public AddressModel Address { get; set; } = new AddressModel();
        public ApplicationUserListModel User { get; set; }
    }
}