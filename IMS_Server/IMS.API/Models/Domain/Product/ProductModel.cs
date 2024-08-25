using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.API.Models.Domain.Product
{
    public class ProductModel
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(1, 1000)]
        public double Price { get; set; }

        public int AvailableQuantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
       
    }
}
