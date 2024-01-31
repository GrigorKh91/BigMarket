namespace BigMarket.Services.EmailAPI.Models.Dto
{
    public sealed class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetalisDto> CartDetalis { get; set; }
    }
}
