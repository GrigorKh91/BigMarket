using BigMarket.Web.Models.ProductAPI;

namespace BigMarket.Web.Models.ShoppingCartAPI
{
    public sealed class CartDetalisDto
    {
        public int CartDetalisId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto CartHeaderDto { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
