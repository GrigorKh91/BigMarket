using BigMarket.Services.AuthAPI.Models;

namespace BigMarket.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
