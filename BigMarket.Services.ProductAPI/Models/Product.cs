using System.ComponentModel.DataAnnotations;

namespace BigMarket.Services.ProductAPI.Models
{
    public sealed class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

         [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }

        [MaxLength(200)]
        public string ImageLocalPath { get; set; }
    }
}
