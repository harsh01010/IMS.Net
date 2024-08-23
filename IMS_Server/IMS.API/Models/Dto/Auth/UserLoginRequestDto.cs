using System.ComponentModel.DataAnnotations;

namespace IMS.API.Models.Dto.Auth
{
    public class UserLoginRequestDto
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
