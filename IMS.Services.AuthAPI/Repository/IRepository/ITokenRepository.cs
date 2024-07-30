

using IMS.Services.AuthAPI.Models.Domain;

namespace IMS.Services.AuthAPI.Repository.IRepository
{
    public interface ITokenRepository
    {
        public string CreateJWTToken( ApplicationUser user , List<string> roles);
    }
}
