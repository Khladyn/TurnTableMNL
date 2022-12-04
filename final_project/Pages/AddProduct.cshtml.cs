using final_project.Data;
using final_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace final_project.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class AddProductModel : PageModel
    {

        [BindProperty]
        public ProductModel NewProduct { get; set; }
        public ProductModel OldProduct { get; set; }

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

        public void OnGetEdit(int id)
        {
            NewProduct = new ProductModel();
            OldProduct = _context.Products.Find(id);
            NewProduct = OldProduct;
        }

        public async Task OnPostAsync()
        {
            
            OldProduct = _context.Products.FirstOrDefault(p => p.ProductID == NewProduct.ProductID);

            fileupload = Request.Form.Files[0];

            NewProduct.CoverArt = @"images\" + Path.GetFileName(fileupload.FileName);

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\images", fileupload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await fileupload.CopyToAsync(fileStream);
            }

            //if (OldProduct == null)
            //{
            //    _context.Products.Add(NewProduct);
            //}
            //else
            //{
                OldProduct = NewProduct;
                _context.Products.Update(OldProduct);
            //}
            
            _context.SaveChanges();
        }

    }
}
