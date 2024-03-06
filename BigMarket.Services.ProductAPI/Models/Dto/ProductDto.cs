namespace BigMarket.Services.ProductAPI.Models.Dto
{
    public sealed class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageLocalPath { get; set; }
        public IFormFile Image { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(ProductDto))
            {
                return false;
            }
            ProductDto product_to_compare = (ProductDto)obj;

            return ProductId == product_to_compare.ProductId && Name == product_to_compare.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
