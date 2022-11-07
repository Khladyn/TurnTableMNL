using System.ComponentModel.DataAnnotations;

namespace final_project.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateJoined { get; set; }
        public string Role { get; set; }

    }
}
