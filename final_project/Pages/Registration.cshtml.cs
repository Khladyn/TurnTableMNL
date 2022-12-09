using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Security.Claims;

namespace final_project.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public UserModel User { get; set; }
        public UserModel oldUser { get; set; }
        public string username { get; set; }

        private readonly DatabaseContext _context;

        public RegistrationModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Registration";

            User = new UserModel();
        }

        public void OnGetView(string username)
        {
            TempData["Editable"] = "disabled";
            ViewData["Title"] = "User Profile";

            User = new UserModel();

            username = HttpContext.Request.Query["id"];

            oldUser = _context.Users.FirstOrDefault(o => o.Username == username);

            User = oldUser;
        }

        public void OnGetEdit(string username)
        {
            ViewData["Title"] = "User Profile";

            User = new UserModel();

            username = HttpContext.Request.Query["id"];

            oldUser = _context.Users.FirstOrDefault(o => o.Username == username);

            User = oldUser;
        }

        public IActionResult OnPost()
        {
            ViewData["Title"] = "Registration";

            if (!ModelState.IsValid) return Page();

            UserModel tempUser = _context.Users
                .Where(t => t.Username == User.Username)
                .FirstOrDefault();

            if (tempUser != null)
            {
                TempData["Error"] = "Username is already taken.";
                return Page();
            } else
            {
                User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);
                User.Repassword = BCrypt.Net.BCrypt.HashPassword(User.Repassword);
                _context.Users.Add(User);
                _context.SaveChanges();
            }

            return RedirectToPage("Login");
        }

        public IActionResult OnPostUpdate(string username)
        {
            //if (!ModelState.IsValid) return RedirectToAction("Edit");

            username = HttpContext.Request.Query["id"];

            oldUser = _context.Users.FirstOrDefault(o => o.Username == username);

                oldUser.Username = User.Username;
                oldUser.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);
                oldUser.Repassword = BCrypt.Net.BCrypt.HashPassword(User.Repassword);
                oldUser.Email = User.Email;
                oldUser.Prefix = User.Prefix;
                oldUser.FirstName = User.FirstName;
                oldUser.LastName = User.LastName;
                oldUser.Address = User.Address;
                oldUser.ContactNumber = User.ContactNumber;
                oldUser.Birthdate = User.Birthdate;
                oldUser.Role = User.Role;

                _context.Users.Update(oldUser);
                _context.SaveChanges();

            return Redirect("Logout");
        }
    }
}
