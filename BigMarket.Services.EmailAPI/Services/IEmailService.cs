using BigMarket.Services.EmailAPI.Models.Dto;

namespace BigMarket.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
    }
}
