﻿using BigMarket.Services.OrderAPI.Models.Dto;
using BigMarket.Services.OrderAPI.Services.IServices;
using Newtonsoft.Json;

namespace BigMarket.Services.OrderAPI.Service
{
    public sealed class ProductService(IHttpClientFactory httpClientFactory) : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
