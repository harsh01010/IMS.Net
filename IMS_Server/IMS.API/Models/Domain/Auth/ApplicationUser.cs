using Microsoft.AspNetCore.Identity;

namespace IMS.API.Models.Domain.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }


    }
}
