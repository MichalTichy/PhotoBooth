using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.Models.User
{
    public class ApplicationUserListModel : ModelBase
    {
        [Required(ErrorMessage = "Zadajte meno")]
        [MinLength(3, ErrorMessage = "Zadajte aspon 3 pismena")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Zadajte priezvisko")]
        [MinLength(3, ErrorMessage = "Zadajte aspon 3 pismena")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Zadajte e-mailovu adresu")]
        [EmailAddress(ErrorMessage = "Zadajte e-mailovu adresu v spravnom tvare")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadajte tel. c.")]
        [RegularExpression("(([+]|[0]{2})[4][2][01][9])[0-9]{8}|([0][9])[0-9]{8}|([+][4][2][1][ ])[9][0-9]{2}[ ][0-9]{3}[ ][0-9]{3}", ErrorMessage = "Neplatny format tel. cisla")]
        public string PhoneNumber { get; set; }
    }
}