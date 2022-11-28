using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace final_project.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Numbers and letters only.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Must be at least eight characters and include a number, a symbol, and an uppercase letter")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Retype Password")]
        [Compare("Password", ErrorMessage = "Password above should match")]
        public string Repassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string Prefix { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Alphabetic characters only.")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Alphabetic characters only.")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateJoined { get; set; }

        [DataType(DataType.Text)]
        public string Role { get; set; }

    }
}
