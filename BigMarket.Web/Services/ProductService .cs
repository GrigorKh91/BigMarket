using BigMarket.Web.Models;
using BigMarket.Web.Models.ProductAPI;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Services
{
    public sealed class ProductService(IBaseService baseService) : IProductService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> CreateProductAsync(ProductDto ProductDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = ProductDto,
                Url = SD.ProductAPIBase + "/api/product/",
                ContentType = SD.ContentType.MultipartFormData

            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto ProductDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = ProductDto,
                Url = SD.ProductAPIBase + "/api/product/",
                ContentType = SD.ContentType.MultipartFormData
            };
            return await _baseService.SendAsync(request);
        }
    }
}
