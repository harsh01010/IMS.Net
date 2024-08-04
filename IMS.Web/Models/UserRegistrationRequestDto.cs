using System.ComponentModel.DataAnnotations;

namespace IMS.Web.Models
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
   
        public string Role {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
