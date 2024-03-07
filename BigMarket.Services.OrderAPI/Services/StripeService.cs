using BigMarket.Services.OrderAPI.Models;
using BigMarket.Services.OrderAPI.Models.Dto;
using BigMarket.Services.OrderAPI.Services.IServices;
using Stripe;
using Stripe.Checkout;

namespace BigMarket.Services.OrderAPI.Services
{
    public class StripeService : IStripeService
    {
        public void CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in stripeRequestDto.OrderHeader.OrderDetalis)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // $20.99  => 2099
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        },
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            if (stripeRequestDto.OrderHeader.Discount > 0)
            {
                var discountsObj = new List<SessionDiscountOptions>()
                    {
                        new SessionDiscountOptions()
                        {
                           Coupon=stripeRequestDto.OrderHeader.CouponCode
                        }
                    };
                options.Discounts = discountsObj;
            }
            var service = new Stripe.Checkout.SessionService();
            Session session = service.Create(options);
            stripeRequestDto.SessionUrl = session.Url;
            stripeRequestDto.SessionId = session.Id;
        }
        public StripePaymentIntent GetPaymentIntentInfo(string stripeSessionId)
        {
            var service = new Stripe.Checkout.SessionService();
            Session session = service.Get(stripeSessionId);
            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
            var paymentIntentInfo = new StripePaymentIntent
            {
                Id = paymentIntent.Id,
                Status = paymentIntent.Status
            };
            return paymentIntentInfo;
        }
    }
}
