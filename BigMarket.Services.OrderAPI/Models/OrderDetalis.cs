using BigMarket.Services.OrderAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigMarket.Services.OrderAPI.Models
{
    public sealed class OrderDetalis
    {
        [Key]
        public int OrderDetalisId { get; set; }
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }

        [MaxLength(450)]
        public string ProductName { get; set; }

        public double Price { get; set; }   
    }
}
