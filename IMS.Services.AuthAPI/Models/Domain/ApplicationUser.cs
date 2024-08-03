using Microsoft.AspNetCore.Identity;

namespace IMS.Services.AuthAPI.Models.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string Name {  get; set; }
        

    }
}
