namespace BigMarket.Services.OrderAPI.Models
{
    public sealed class StripePaymentIntent
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
