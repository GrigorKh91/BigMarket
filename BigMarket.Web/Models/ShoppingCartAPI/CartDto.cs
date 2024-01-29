namespace BigMarket.Web.Models.ShoppingCartAPI
{
    public sealed class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetalisDto> CartDetalis { get; set; }
    }
}
