using System.ComponentModel.DataAnnotations;

namespace BigMarket.Services.RewardAPI.Models
{
    public sealed class Rewards
    {
        public int Id { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }
        public DateTime RewardsDate { get; set; }
        public int RewardsActivity { get; set; }
        public int OrderId { get; set; }
    }
}
