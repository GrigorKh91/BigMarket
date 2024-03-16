using BigMarket.Services.ProductAPI.Services.IServices;
using BigMarket.Services.ProductAPI.Core.Models.Dto;
using BigMarket.Services.ProductAPI.Core.Models;
using ProductAPI.Core.Services.IServices;
using AutoMapper;



namespace BigMarket.Services.ProductAPI.Services
{
    public class ProductService(IProductRepository db, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _db = db;
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            try
            {
                IEnumerable<Product> ProductList = await _db.GetAllProductsAsync();
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
                var Product = await _db.GetProductByIdAsync(id);
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
                ArgumentNullException.ThrowIfNull(productDto);

                Product product = _mapper.Map<Product>(productDto);
                await _db.CreateProductAsync(product);
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
                product = _db.UpdateProductAsync(product);
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
                        FileInfo file = new(oltFilePathDirectory);
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

                product = _db.UpdateProductAsync(product);
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
                Product product = await _db.GetProductByIdAsync(id);
                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var oltFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo file = new(oltFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }

                await _db.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDto> GetAllProductsNameAsync()
        {
            try
            {
                IEnumerable<string> ProductList = await _db.GetAllProductsNameAsync();

                _response.Result = ProductList;
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
