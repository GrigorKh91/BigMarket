using BigMarket.Web.Models;
using BigMarket.Web.Models.AuthApi;
using BigMarket.Web.Service.IService;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Service
{
    public sealed class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
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
    }
}
