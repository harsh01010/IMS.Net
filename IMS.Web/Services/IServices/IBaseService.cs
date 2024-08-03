using IMS.Web.Models;

namespace IMS.Web.Services.IServices
{
    public interface IBaseservice
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
