using IMS.Web.Services.IServices;
using IMS.Web.Utility;

namespace IMS.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(StaticDetails.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.TokenCookie, out token);
            return hasToken is true ? token : null;
        }
        public string? GetId()
        {
            string? id = null;
            bool? hasId = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticDetails.UserId, out id);

            return hasId is true ? id : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(StaticDetails.TokenCookie, token);

        }

        public void SetId(string id)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append("userId", id);
        }
        public void SetRole(string role)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append("role", role);
        }
        public string GetRole()
        {
            string? role = null;
            bool? hasRole = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("role", out role);

            return role;
        }
            
    }
}
