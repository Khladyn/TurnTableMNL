using final_project.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace final_project.Pages
{
    public class LoginModel : PageModel
    {

        private readonly DatabaseContext _context;

        [BindProperty]
        public Credentials Credential { get; set; }

        public LoginModel(DatabaseContext context)
        {
            _context = context;
        }

        private SignInManager<IdentityUser> signInManager;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var acc = _context.Users.SingleOrDefault(x => x.Username == Credential.Username);

            if (acc != null && BCrypt.Net.BCrypt.Verify(Credential.Password, acc.Password) && acc.Role == "customer")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "customer"),
                    new Claim(ClaimTypes.Name, Credential.Username)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            else if (acc != null && BCrypt.Net.BCrypt.Verify(Credential.Password, acc.Password) && acc.Role == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Name, Credential.Username),
                    new Claim("Admin", "true")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            return Page();
        }

        public class Credentials
        {
            [Required]
            [DisplayName("Username")]
            public string Username { get; set; }

            [Required]
            [DisplayName("Password")]
            public string Password { get; set; }

            [DisplayName("Remember Me")]
            public bool Remember { get; set; }

        }

    }
}
