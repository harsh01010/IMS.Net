using System.ComponentModel.DataAnnotations;

namespace IMS.Web.Models
{
	public class ProductDto
	{
		public Guid ProductId { get; set; }
		public string Name { get; set; } = string.Empty;
		public double Price { get; set; }
		public string Description { get; set; } = string.Empty;
		public string CategoryName { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		public string? ImageLocalPath { get; set; }
		public IFormFile? Image { get; set; }


		[Range(1,100)]
		public int Count { get; set; } = 1;
    }
}
