using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace final_project.Models
{
    public class OrderModel
    {
        
        [Key]
        public int OrderID { get; set; }

        public string CoverArt { get; set; }

        public string Album { get; set; }

        public string Artist { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public double Subtotal { get { return Quantity * Price; } }

        public DateTime DateAdded { get; set; }

        public int UserID { get; set; }

        public OrderModel() {}

        public OrderModel(ProductModel product)
        {
            OrderID = product.ProductID;
            CoverArt = product.CoverArt;
            Album = product.Album;
            Artist = product.Artist;
            Type = product.Type;
            Price = product.Price;
            Quantity = 1;
            DateAdded = DateTime.Now;
        }

    }

}
