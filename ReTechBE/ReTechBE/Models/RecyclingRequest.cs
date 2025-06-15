using System;
using System.ComponentModel.DataAnnotations;

namespace ReTechApi.Models
{
    public class RecyclingRequest
    {
        public string Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public string? AssignedCompanyId { get; set; }

        [Required]
        public string ItemDescription { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal EstimatedValue { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? Notes { get; set; }

        public string DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceCondition { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PickupDate { get; set; }
        public string PickupAddress { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Assigned,
        Scheduled,
        InProgress,
        Completed,
        Cancelled
    }
} 