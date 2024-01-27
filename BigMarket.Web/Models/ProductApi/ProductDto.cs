using System.ComponentModel.DataAnnotations;

namespace BigMarket.Web.Models.ProductApi
{
    public sealed class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

        [Range(1, 100)] // TODO check count
        public int Count { get; set; } = 1;
    }
}
