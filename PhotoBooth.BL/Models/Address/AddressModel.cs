using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.Models.Address
{
    public class AddressModel : ModelBase
    {
        [Required(ErrorMessage = "Musíte zadat obec!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Musíte zadat PSČ!")]
        public string PostalCode { get; set; }

        public string Street { get; set; }

        [Required(ErrorMessage = "Musíte zadat číslo domu!")]
        public string BuildingNumber { get; set; }
    }
}