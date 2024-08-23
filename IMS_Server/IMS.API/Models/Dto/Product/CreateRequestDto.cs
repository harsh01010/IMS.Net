namespace IMS.API.Models.Dto.Product
{
	public class CreateRequestDto
	{
		public string Name { get; set; } = string.Empty;
		public double Price { get; set; }
		public string Description { get; set; } = string.Empty;
		public string CategoryName { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		
	}
}
