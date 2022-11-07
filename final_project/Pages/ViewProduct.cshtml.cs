using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
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
        }
    }
}
