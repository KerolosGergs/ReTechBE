using ReTechApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ReTechBE.Models
{
    
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        public int StockQuantity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsNew { get; set; }
    }
}
