using Microsoft.EntityFrameworkCore;
using BigMarket.Services.RewardAPI.Data;
using BigMarket.Services.RewardAPI.Message;
using BigMarket.Services.RewardAPI.Models;

namespace BigMarket.Services.RewardAPI.Services
{
    public sealed class RewardService(DbContextOptions<AppDbContext> dbOptions) : IRewardService
    {
        private readonly DbContextOptions<AppDbContext> _dbOptions = dbOptions;

        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new Rewards()
                {
                    OrderId = rewardsMessage.OrderId,
                    RewardsActivity = rewardsMessage.RewardsActivity,
                    UserId = rewardsMessage.UserId,
                    RewardsDate = DateTime.Now,
                };

                await using var _db = new AppDbContext(_dbOptions);
                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
