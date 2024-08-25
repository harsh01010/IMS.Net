namespace IMS.API.Models.Dto.Product
{
	public class CreateRequestDto
	{
		public string Name { get; set; } = string.Empty;
		public double Price { get; set; }
		public string Description { get; set; } = string.Empty;
		public Guid CategoryID { get; set; }
		public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public IFormFile? Image { get; set; }

    }
}
