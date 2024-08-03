using System.ComponentModel.DataAnnotations;

namespace IMS.Web.Models
{
    public class UserLoginRequestDto
    {
        [Required]
        public string UserName {  get; set; }


        [Required]
        public string Password { get; set; }
    }
}
