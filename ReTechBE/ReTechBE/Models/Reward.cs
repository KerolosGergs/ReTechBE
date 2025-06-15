using System;
using System.ComponentModel.DataAnnotations;

namespace ReTechApi.Models
{
    public class Reward
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public decimal Points { get; set; }

        [Required]
        public RewardType Type { get; set; }

        public string? Description { get; set; }

        public decimal Value { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedDate { get; set; }
    }

    public enum RewardType
    {
        RecyclingPoints,
        ReferralBonus,
        SpecialOffer,
        LoyaltyReward
    }
} 