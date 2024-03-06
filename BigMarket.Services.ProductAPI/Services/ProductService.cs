using BigMarket.Services.ProductAPI.Services.IServices;
using AutoMapper;
using BigMarket.Services.ProductAPI.Data;
using BigMarket.Services.ProductAPI.Models;
using BigMarket.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;


namespace BigMarket.Services.ProductAPI.Services
{
    public class ProductService(AppDbContext db, IMapper mapper) : IProductService
    {
        private readonly AppDbContext _db = db;
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            try
            {
                IEnumerable<Product> ProductList = await _db.Products.ToListAsync();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(ProductList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
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

        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto, string baseUrl)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                if (productDto.Image != null)
                {
                    string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productDto.Image.CopyTo(fileStream);
                    }
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                else
                {
                    product.ImageUrl = "https://placehold.co/600x400"; // TODO change to some default
                }
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public ResponseDto UpdateProductAsync(ProductDto productDto, string baseUrl)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);

                if (productDto.Image != null)
                {
                    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        var oltFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo file = new FileInfo(oltFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = product.ProductId + Path.GetExtension(productDto.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        productDto.Image.CopyTo(fileStream);
                    }
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }

                _db.Products.Update(product);// TODO check need or not async
                _db.SaveChanges();
                _response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            try
            {
                Product product = await _db.Products.FirstAsync(c => c.ProductId == id);
                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var oltFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo file = new FileInfo(oltFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                _db.Products.Remove(product);// TODO check need or not async
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
