using BigMarket.Web.Models.ProductAPI;

namespace BigMarket.Web.Models.OrderAPI
{
    public sealed class OrderDetalisDto
    {
        public int OrderDetalisId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
