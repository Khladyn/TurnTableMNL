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

        [BindProperty]
        public List<OrderModel> cart { get; set; }

        public double total { get; set; }

        public string emptyCart { get; set; } = "Your cart is empty.";

        public CartModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart") ?? new List<OrderModel>();
        }

        public async Task<IActionResult> OnGetPay()
        {
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart");

            HttpContext.Session.Remove("Cart");

            foreach (var order in cart)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }

            return RedirectToAction("OnGet");
        }

        public async Task<IActionResult> OnGetRemove(int id)
        {
            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart");

            cart.RemoveAll(c => c.ProductID == id);

            HttpContext.Session.SetJson("Cart", cart);

            return RedirectToAction("OnGet");

        }

        public async Task<IActionResult> OnGetAdd(int id)
        {
            var product = await _context.Products.FindAsync(id);

            var username = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            var user = _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            cart = HttpContext.Session.GetJson<List<OrderModel>>("Cart") ?? new List<OrderModel>();

            OrderModel order = cart.Where(c => c.ProductID == id).FirstOrDefault();

            if (order == null)
            {
                cart.Add(new OrderModel(product, user));
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
