using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DutchTreat.Data.Entities;

namespace DutchTreat.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public string ProductArtDescription { get; set; }
        public string ProductArtId { get; set; }
        public string ProductArtist { get; set; }
        // public Product Product { get; set; }
    }
}