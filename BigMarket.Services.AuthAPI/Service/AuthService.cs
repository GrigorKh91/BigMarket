using BigMarket.Services.AuthAPI.Data;
using BigMarket.Services.AuthAPI.Models;
using BigMarket.Services.AuthAPI.Models.Dto;
using BigMarket.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BigMarket.Services.AuthAPI.Service
{
    public sealed class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(AppDbContext db, 
                                        UserManager<ApplicationUser> userManager, 
                                        RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        Task<LoginResponseDto> IAuthService.Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        Task<UserDto> IAuthService.Register(RegistrationRequestDto registrationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
