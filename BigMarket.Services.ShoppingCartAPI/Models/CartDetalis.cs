using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BigMarket.Services.ShoppingCartAPI.Models.Dto;

namespace BigMarket.Services.ShoppingCartAPI.Models
{
    public sealed class CartDetalis
    {
        [Key]
        public int CartDetalisId { get; set; }
        public int CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
