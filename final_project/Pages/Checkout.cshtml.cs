using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace final_project.Pages
{
    [Authorize]
    [BindProperties]
    public class CheckoutModel : PageModel
    {
        private readonly DatabaseContext _context;
        public string ModeOfPayment { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderModel> cart { get; set; }
        public UserModel User { get; set; }

        public CheckoutModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            User = _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

        }

        public async Task<IActionResult> OnPostAsync()
        {

            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart") ?? new List<OrderModel>();

            foreach (var c in cart)
            {
                if (ModeOfPayment == "Cash on delivery")
                {
                    OrderStatus = "Pending payment";
                }
                else
                {
                    OrderStatus = "Paid";
                }

                c.OrderStatus = OrderStatus;
                c.ModeOfPayment = ModeOfPayment;

                _context.Orders.Add(c);
                _context.SaveChanges();
            }

            HttpContext.Session.SetJson("Cart", cart);

            HttpContext.Session.Remove("Cart");

            return Redirect("OrdersList");
        }

    }
}
