using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    [Authorize]
    public class OrdersList : PageModel
    {
        private readonly DatabaseContext _context;

        public OrdersList(DatabaseContext context)
        {
            _context = context;
        }

        public List<OrderModel> orders;

        public string emptyOrder { get; set; } = "No orders at the moment.";

        public double total { get; set; }

        public void OnGet()
        {
            string name = HttpContext.User.Identity.Name;

            if ( name == "admin")
            {
                orders = _context.Orders.ToList();
            } else
            {
                orders = _context.Orders
                    .Where(o => o.Username == name).ToList();
            }
            
        }

        public IActionResult OnGetDelete(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}
