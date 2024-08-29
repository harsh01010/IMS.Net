using Quartz;
using System.Threading.Tasks;
using System.Linq;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Models.Dto;
using Microsoft.Extensions.Logging;
using System.Text; // Import for StringBuilder

namespace IMS.API.Jobs
{
    public class ProductQuantityCheckJob : IJob
    {
        private readonly IEmailSender emailSender;
        private readonly IProductRepository productRepository;
        private readonly ILogger<ProductQuantityCheckJob> _logger;

        // Constructor
        public ProductQuantityCheckJob(IEmailSender emailSender, IProductRepository productRepository, ILogger<ProductQuantityCheckJob> logger)
        {
            this.emailSender = emailSender;
            this.productRepository = productRepository;
            this._logger = logger;
        }

        // Async method for executing the job
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executing ProductQuantityCheckJob at {time}", DateTime.Now);

            // Fetch products asynchronously
            var products = await productRepository.GetAllAsync();
            var lowStockProducts = products.Where(p => p.AvailableQuantity <= 0).ToList();

            if (lowStockProducts.Any())
            {
                _logger.LogInformation("Found {count} products with low or zero quantity", lowStockProducts.Count);

                // Generate a list of product names
                var productNames = new StringBuilder();
                productNames.AppendLine("The following products have low or zero quantity:");
                foreach (var product in lowStockProducts)
                {
                    productNames.AppendLine($"- {product.Name} (Available Quantity: {product.AvailableQuantity})");
                }

                // Prepare the email request
                var emailRequest = new SendEmailRequestDto
                {
                    Email = "dhruv.sharma2414@gmail.com",
                    Subject = "Low Stock Alert",
                    Body = productNames.ToString() // Include the product list in the email body
                };

                // Send the alert email
                await emailSender.EmailSendAsync(emailRequest);
                _logger.LogInformation("Alert email sent to {email}", emailRequest.Email);
            }
            else
            {
                _logger.LogInformation("No products with low quantity found.");
            }
        }
    }
}
