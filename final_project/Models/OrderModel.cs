using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace final_project.Models
{
    public class OrderModel
    {
        
        [Key]
        public int OrderID { get; set; }
        public int ProductID { get; set; }
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
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Mode of Payment")]
        public string ModeOfPayment { get; set; }
        [DataType(DataType.Text)]
        public string OrderStatus { get; set; }

        public OrderModel() {}

        public OrderModel(ProductModel product, UserModel user)
        {
            ProductID = product.ProductID;
            CoverArt = product.CoverArt;
            Album = product.Album;
            Artist = product.Artist;
            Type = product.Type;
            Price = product.Price;
            Quantity = 1;
            DateAdded = DateTime.Now;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Address = user.Address;
            ContactNumber = user.ContactNumber;
        }

    }

}
