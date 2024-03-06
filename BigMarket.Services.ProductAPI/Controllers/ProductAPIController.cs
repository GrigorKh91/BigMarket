﻿using BigMarket.Services.ProductAPI.Models.Dto;
using BigMarket.Services.ProductAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            var response = await _productService.GetAllProductsAsync();
            return response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]  // TODO change from had code
        public async Task<ResponseDto> Post(ProductDto productDto)
        {

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            var response = await _productService.CreateProductAsync(productDto, baseUrl);
            return response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Put(ProductDto productDto)
        {

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
            var response = _productService.UpdateProductAsync(productDto, baseUrl);
            return response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public async Task<ResponseDto> Delete(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            return response;
        }
    }
}
