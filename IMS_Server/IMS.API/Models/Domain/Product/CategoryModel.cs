using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IMS.API.Models.Domain.Product
{
    public class CategoryModel
    {
        [Key]
        public Guid CategoryId { get; set; }    
        public string CategoryName { get; set; }
    }
}
