using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public UserModel User { get; set; }

        private readonly DatabaseContext _context;

        public RegistrationModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            User = new UserModel();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);
            User.Repassword = BCrypt.Net.BCrypt.HashPassword(User.Repassword);
            _context.Users.Add(User);
            _context.SaveChanges();
            return RedirectToPage("Login");
        }
    }
}
