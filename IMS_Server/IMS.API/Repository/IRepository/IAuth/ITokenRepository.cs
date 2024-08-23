using IMS.API.Models.Domain.Auth;

namespace IMS.API.Repository.IRepository.IAuth
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
