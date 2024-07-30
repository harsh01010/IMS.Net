using System.ComponentModel.DataAnnotations;

namespace IMS.Services.AuthAPI.Models.Dto
{
    public class UserLoginRequestDto
    {
        [Required]
        public string UserName {  get; set; }


        [Required]
        public string Password { get; set; }
    }
}
