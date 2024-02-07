using BigMarket.Services.RewardAPI.Message;

namespace BigMarket.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
