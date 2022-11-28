using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    [Authorize]
    public class VinylsCatalogModel : PageModel
    {
        private readonly DatabaseContext _context;

        public VinylsCatalogModel(DatabaseContext context)
        {
            _context = context;
        }

        public List<ProductModel> Vinyls;

        public int product_id { get; set; }


        public void OnGet()
        {
            Vinyls = _context.Products.ToList();
        }

        public IActionResult OnGetDelete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToPage("VinylsCatalog");

        }
    }
}
