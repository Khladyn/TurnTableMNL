using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace final_project.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; }

        public string Type { get; set; }

        [StringLength (75, ErrorMessage = "Artist name cannot exceed 75 characters.")]
        public string Artist { get; set; }

        [StringLength(250, ErrorMessage = "Album name cannot exceed 250 characters.")]
        public string Album { get; set; }

        public string CoverArt { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public string Format { get; set; }

        public string Genre { get; set; }

        public string Tracks { get; set; }

        public int TracksTotal { get; set; }

        public int Sides { get; set; }

        [StringLength(75, ErrorMessage = "Label cannot exceed 75 characters.")]
        public string Label { get; set; }

        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
