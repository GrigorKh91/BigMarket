using BigMarket.Web.Models.OrderAPI;

namespace BigMarket.Web.Models
{
    public sealed class StripeRequestDto
    {
        public string SessionUrl { get; set; }
        public string SessionId { get; set; }
        public string ApprovedUrl { get; set; }
        public string CancelUrl { get; set; }
        public OrderHeaderDto OrderHeader { get; set; }
    }
}
