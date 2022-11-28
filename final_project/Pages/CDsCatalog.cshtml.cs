using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages.Shared
{
    [Authorize]
    public class CDsCatalogModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CDsCatalogModel(DatabaseContext context)
        {
            _context = context;
        }

        public List<ProductModel> CDs;

        public int product_id { get; set; }


        public void OnGet()
        {
            CDs = _context.Products.ToList();
        }

        public IActionResult OnGetDelete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToPage("CDsCatalog");

        }
    }
}
