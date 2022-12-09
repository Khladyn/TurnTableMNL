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
        public int id { get; set; }

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
            ViewData["Title"] = "Add Product";

            NewProduct = new ProductModel();
        }
        public void OnGetEdit(int id)
        {
            ViewData["Title"] = "Edit Product";

            NewProduct = new ProductModel();
            OldProduct = _context.Products.Find(id);
            NewProduct = OldProduct;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if(fileupload == null)
            {
                NewProduct.CoverArt = @"images\music_placeholder.png";
            }
            else
            {
                fileupload = Request.Form.Files[0];
                NewProduct.CoverArt = @"images\" + Path.GetFileName(fileupload.FileName);

                var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\images", fileupload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await fileupload.CopyToAsync(fileStream);
                }
            }

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["id"]))
            {

                id = int.Parse(HttpContext.Request.Query["id"]);
                OldProduct = _context.Products.FirstOrDefault(p => p.ProductID == id);

                OldProduct.Type = NewProduct.Type;
                OldProduct.Artist = NewProduct.Artist;
                OldProduct.Album = NewProduct.Album;
                OldProduct.CoverArt = NewProduct.CoverArt;
                OldProduct.Description = NewProduct.Description;
                OldProduct.Format = NewProduct.Format;
                OldProduct.Genre = NewProduct.Genre;
                OldProduct.Tracks = NewProduct.Tracks;
                OldProduct.TracksTotal = NewProduct.TracksTotal;
                OldProduct.Sides = NewProduct.Sides;
                OldProduct.Label = NewProduct.Label;
                OldProduct.ReleaseDate = NewProduct.ReleaseDate;
                OldProduct.Price = NewProduct.Price;

                _context.Products.Update(OldProduct);
                _context.SaveChanges();

                return Redirect("ViewProduct?id=" + id.ToString());
            }
            else
            {
                _context.Products.Add(NewProduct);
                _context.SaveChanges();

                var latestEntry = _context.Products
                    .OrderBy(le => le.ProductID)
                    .Last();

                return Redirect("ViewProduct?id=" + latestEntry.ProductID);
            }
        }

    }
}
