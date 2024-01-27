using AutoMapper;
using BigMarket.Services.ProductAPI.Data;
using BigMarket.Services.ProductAPI.Models;
using BigMarket.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController] 
    public class ProductAPIController(AppDbContext db, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _db = db;
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> ProductList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(ProductList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var Product = await _db.Products.FirstAsync(c => c.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(Product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]  // TODO change from had code
        public async Task<ResponseDto> Post([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product Product = _mapper.Map<Product>(ProductDto);
                await _db.Products.AddAsync(Product);
                await _db.SaveChangesAsync();
                _response.Result = ProductDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Put([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product Product = _mapper.Map<Product>(ProductDto);
                _db.Products.Update(Product);// TODO check need or not async
                _db.SaveChanges();
                _response.Result = ProductDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]  // TODO change from hatd code
        public ResponseDto Delete(int id)
        {
            try
            {
                Product Product = _db.Products.First(c => c.ProductId == id);
                _db.Products.Remove(Product);// TODO check need or not async
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
