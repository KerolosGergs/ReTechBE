using System;
using System.ComponentModel.DataAnnotations;

namespace ReTechApi.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string Image { get; set; }
        public bool IsNew { get; set; }
    }

    public enum ProductCategory
    {
        Electronics,
        Appliances,
        Computers,
        MobilePhones,
        Other
    }
} 