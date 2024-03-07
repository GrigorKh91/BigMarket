using BigMarket.Services.OrderAPI.Models;
using BigMarket.Services.OrderAPI.Models.Dto;

namespace BigMarket.Services.OrderAPI.Services.IServices
{
    public interface IStripeService
    {
        void CreateStripeSession(StripeRequestDto stripeRequestDto);
        StripePaymentIntent GetPaymentIntentInfo(string stripeSessionId);
    }
}
