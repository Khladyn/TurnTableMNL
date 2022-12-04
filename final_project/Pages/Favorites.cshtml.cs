using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Xml.Linq;

namespace final_project.Pages
{
    public class Favorites : PageModel
    {
        private readonly DatabaseContext _context;

        public List<ProductModel> favorites { get; set; }

        public string itemAdded { get; set; }

        public string emptyFavorites { get; set; } = "You have no favorites.";

        public Favorites(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            favorites = HttpContext.Session.GetJson<List<ProductModel>>("Favorites") ?? new List<ProductModel>();
        }

        public async Task<IActionResult> OnGetAdd(int id)
        {
            var product = await _context.Products.FindAsync(id);

            favorites = HttpContext.Session.GetJson<List<ProductModel>>("Favorites") ?? new List<ProductModel>();

            ProductModel favorite = favorites.Where(f => f.ProductID == id).FirstOrDefault();

            if (favorite == null)
            {
                favorites.Add(product);
                TempData["Success"] = "Item has been added to Favorites!";
            }
            else
            {
                TempData["Success"] = "Item is already added";
            }

            HttpContext.Session.SetJson("Favorites", favorites);

            return Redirect(Request.Headers["Referer"].ToString());

        }

        public async Task<IActionResult> OnGetRemove(int id)
        {
            favorites = HttpContext.Session.GetJson<List<ProductModel>>("Favorites");

            favorites.RemoveAll(c => c.ProductID == id);

            HttpContext.Session.SetJson("Favorites", favorites);

            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}
