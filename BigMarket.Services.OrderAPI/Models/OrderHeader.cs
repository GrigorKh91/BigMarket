using System.ComponentModel.DataAnnotations;

namespace BigMarket.Services.OrderAPI.Models
{
    public sealed class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public double OrderTotal { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime OrderTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }   // TODO should to change type

        [MaxLength(450)]
        public string PaymentIntentId { get; set; }

        [MaxLength(450)]
        public string StripeSessionId { get; set; }

        public IEnumerable<OrderDetalis> OrderDetalis { get; set; }
    }
}
