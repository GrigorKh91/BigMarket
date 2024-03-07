using BigMarket.Services.OrderAPI.Models.Dto;
namespace BigMarket.Services.OrderAPI.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto> GetByUserIdAsync(bool isAdmin, string userId);
        Task<ResponseDto> GetByOrderIdAsync(int id);
        Task<ResponseDto> CreateAsync(CartDto cartDto);
        Task<ResponseDto> UpdateStatusAsync(int orderId, string newStatus);
        Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto);
        Task<ResponseDto> ValidateStripeSession(int orderHeaderId);
    }
}
