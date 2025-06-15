using System;
using System.Collections.Generic;
using System.Linq;
using ReTechApi.Models;

namespace ReTechApi.Data
{
    public static class RewardRepository
    {
        private static readonly List<Reward> _rewards = new List<Reward>
        {
            new Reward
            {
                Id = "reward_1",
                UserId = "1", // Demo User
                Type = (RewardType)Enum.Parse(typeof(RewardType), "voucher", true),
                Value = 100,
                Description = "E£100 HyperOne Voucher",
                IssueDate = DateTime.UtcNow.AddDays(-7),
                ExpiryDate = DateTime.UtcNow.AddDays(90),
                IsUsed = false,
                TransactionId = "1" // Reference to recycling request
            },
            new Reward
            {
                Id = "reward_2",
                UserId = "1", // Demo User
                Type = (RewardType)Enum.Parse(typeof(RewardType), "voucher", true),
                Value = 150,
                Description = "E£150 HyperOne Voucher",
                IssueDate = DateTime.UtcNow.AddDays(-14),
                ExpiryDate = DateTime.UtcNow.AddDays(60),
                IsUsed = false,
                TransactionId = "2" // Reference to recycling request
            }
        };

        public static IEnumerable<Reward> GetAllRewards()
        {
            return _rewards;
        }

        public static Reward GetRewardById(string id)
        {
            return _rewards.FirstOrDefault(r => r.Id == id);
        }

        public static IEnumerable<Reward> GetRewardsByUserId(string userId)
        {
            return _rewards.Where(r => r.UserId == userId);
        }

        public static Reward AddReward(Reward reward)
        {
            reward.Id = $"reward_{Guid.NewGuid().ToString().Substring(0, 8)}";
            reward.IssueDate = DateTime.UtcNow;
            reward.IsUsed = false;
            _rewards.Add(reward);
            return reward;
        }

        public static Reward UpdateReward(Reward reward)
        {
            var existingReward = _rewards.FirstOrDefault(r => r.Id == reward.Id);
            if (existingReward != null)
            {
                existingReward.IsUsed = reward.IsUsed;
                existingReward.UsedDate = reward.UsedDate;
            }
            return existingReward;
        }

        public static IEnumerable<Reward> GetActiveRewards(string userId)
        {
            var now = DateTime.UtcNow;
            return _rewards.Where(r => 
                r.UserId == userId && 
                !r.IsUsed && 
                r.ExpiryDate > now);
        }
    }
} 