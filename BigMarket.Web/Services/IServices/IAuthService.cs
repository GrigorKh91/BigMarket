using BigMarket.Web.Models;
using BigMarket.Web.Models.AuthApi;

namespace BigMarket.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto  registrationRequestDto);
        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto  registrationRequestDto);
    }
}
