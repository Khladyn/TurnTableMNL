using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    public class AddProductModel : PageModel
    {

        [BindProperty]
        public ProductModel NewProduct { get; set; }

        private readonly DatabaseContext _context;
        private IHostEnvironment _environment;

        public IFormFile fileupload { get; set; }

        public AddProductModel(DatabaseContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public void OnGet()
        {
            NewProduct = new ProductModel();
        }

        public async Task OnPostAsync()
        {

            fileupload = Request.Form.Files[0];

            NewProduct.CoverArt = @"images\" + Path.GetFileName(fileupload.FileName);

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\images", fileupload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await fileupload.CopyToAsync(fileStream);
            }

            _context.Products.Add(NewProduct);
            _context.SaveChanges();
        }

    }
}
