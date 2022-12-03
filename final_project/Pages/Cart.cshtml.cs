using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;
using System.Web;

namespace final_project.Pages
{
    public class CartModel : PageModel
    {

        private readonly DatabaseContext _context;

        public List<OrderModel> cart { get; set; }

        public string EmptyCart { get; set; } = "Your cart is empty.";

        //private readonly UserManager<IdentityUser> _userManager;

        public CartModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart") ?? new List<OrderModel>();
        }

        public void OnGetClear()
        {
            HttpContext.Session.Remove("Cart");
        }

        public async Task<IActionResult> OnGetRemove(int id)
        {
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart");

            cart.RemoveAll(c => c.OrderID == id);

            HttpContext.Session.SetJson("Cart", cart);

            return RedirectToAction("OnGet");

        }

        public async Task<IActionResult> OnGetAdd(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart") ?? new List<OrderModel>();

            OrderModel order = cart.Where(c => c.OrderID == id).FirstOrDefault();

            if (order == null)
            {
                cart.Add(new OrderModel(product));
            }
            else
            {
                order.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());

    }

    }
}
