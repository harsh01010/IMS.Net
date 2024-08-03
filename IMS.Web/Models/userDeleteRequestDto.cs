using System.ComponentModel.DataAnnotations;

namespace IMS.Web.Models
{
    public class userDeleteRequestDto
    {

        [Required]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
