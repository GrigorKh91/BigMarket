using BigMarket.Services.ProductAPI.Core.Models.Dto;
using BigMarket.Services.ProductAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.ProductAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/product")]
    [ApiController]
    public class ProductAPIController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet()]
        public async Task<ResponseDto> Get()
        {
            var response = await _productService.GetAllProductsNameAsync();
            return response;
        }      
    }
}
