using BigMarket.Web.Models;
using BigMarket.Web.Models.AuthAPI;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Services
{
    public sealed class AuthService(IBaseService baseService) : IAuthService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            };
            return await _baseService.SendAsync(request, withBearer: false);
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            };
            return await _baseService.SendAsync(request, withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            ArgumentNullException.ThrowIfNull(registrationRequestDto); // TODO check need or not
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            };
            return await _baseService.SendAsync(request, withBearer: false);
        }

        public async Task<ResponseDto> IsEmailAlreadyRegistered(string email)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = email,
                Url = SD.AuthAPIBase + "/api/auth/IsEmailAlreadyRegistered"
            };
            return await _baseService.SendAsync(request, withBearer: false);
        }

    }
}
