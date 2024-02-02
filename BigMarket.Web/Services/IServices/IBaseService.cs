using BigMarket.Web.Models;

namespace BigMarket.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
