using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    [Authorize]
    public class ViewProductModel : PageModel
    {
        private readonly DatabaseContext _context;

        [BindProperty]
        public ProductModel product { get; set; }
        public string albumname { get; set; }

        public ViewProductModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            product = _context.Products.Find(id);

            if(product.Type == "vinyl")
            {
                TempData["PageRedirect"] = "VinylsCatalog";
            }
            else if (product.Type == "cd")
            {
                TempData["PageRedirect"] = "CDsCatalog";
            }
            else
            {
                TempData["PageRedirect"] = "CassettesCatalog";
            }
        }
    }
}
