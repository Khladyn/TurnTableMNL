using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            await Response.WriteAsync("<script language='javascript'>" +
                "window.alert('Changes saved. Please log back in.');" +
                "window.location='/Index';" +
                "</script>");

            //return RedirectToPage("/Index");
        }
        public async Task OnPostAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            await Response.WriteAsync("<script language='javascript'>" +
                "window.alert('You have been logged out.');" +
                "window.location='Index';" +
                "</script>");

            //return RedirectToPage("/Index");
        }
    }
}
