using BigMarket.Services.ProductAPI.Services.IServices;
using BigMarket.Services.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using BigMarket.Services.ProductAPI.Services;
using BigMarket.Services.ProductAPI.Models;
using EntityFrameworkCoreMock;
using BigMarket.Services.ProductAPI.Models.Dto;
using AutoMapper;
using AutoFixture;

namespace Tests.Products
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private readonly IFixture _fixture;

        public ProductServiceTest()
        {

            _fixture = new Fixture();// not working
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
            });
            var mapper = configuration.CreateMapper();

            var productInitialData = new List<Product>() { };
            DbContextMock<AppDbContext> dbContextMock = new(new DbContextOptionsBuilder<AppDbContext>().Options);

            var dbContex = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Products, productInitialData);

            _productService = new ProductService(dbContex, mapper);
        }


        #region AddProduct

        [Fact]
        public async Task AddProduct_NullProduct()
        {
            //Arrange
            ProductDto productDtoAddRequest = null;

            //Act
            ResponseDto response_from_create = await _productService.CreateProductAsync(productDtoAddRequest, "");

            //Assert
            Assert.True(response_from_create.Message != string.Empty);
        }

        [Fact]
        public async Task AddProduct_ProperProductDetails()
        {
            //Arrange
            ProductDto productDtoAddRequest = new()
            {
                Name = "BMW x5",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 100_000
            };

            //Act
            ResponseDto response_from_create = await _productService.CreateProductAsync(productDtoAddRequest, "");
            ProductDto productDto = response_from_create.Result as ProductDto;

            ResponseDto response = await _productService.GetAllProductsAsync();
            List<ProductDto> productList = response.Result as List<ProductDto>;

            //Assert
            Assert.Contains(productDto, productList);
        }

        #endregion


        #region GetPersonByPersonID

        [Fact]
        public async Task GetProductByID_NullProductID()
        {
            //Arrange
            int productId = default;

            //Act
            ResponseDto product_response_from_get = await _productService.GetProductByIdAsync(productId);

            //Assert
            Assert.Null(product_response_from_get.Result);
        }


        [Fact]
        public async Task GetProductByID_WithNewProductID()
        {
            //Arange
            ProductDto productDtoAddRequest = new()
            {
                Name = "BMW x5",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 100_000
            };

            ResponseDto response_from_create = await _productService.CreateProductAsync(productDtoAddRequest, "");
            ProductDto productDto = response_from_create.Result as ProductDto;


            ResponseDto response_from_get = await _productService.GetProductByIdAsync(productDto.ProductId);
            ProductDto product_response_from_get = response_from_get.Result as ProductDto;

            //Assert
            Assert.Equal(productDtoAddRequest, product_response_from_get);
        }

        #endregion


        #region GetAllProducts

        [Fact]
        public async Task GetAllProducts_EmptyList()
        {
            //Act
            ResponseDto persons_from_get = await _productService.GetAllProductsAsync();
            List<ProductDto> productList = persons_from_get.Result as List<ProductDto>;

            //Assert
            Assert.Empty(productList);
        }


        [Fact]
        public async Task GetAllProducts_AddFewProducts()
        {
            //Arrange
            ProductDto product_request_1 = new()
            {
                Name = "BMW x5",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 100_000
            };
            ProductDto product_request_2 = new()
            {
                Name = "GLE",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 105_000
            };

            await _productService.CreateProductAsync(product_request_1, null);
            await _productService.CreateProductAsync(product_request_2, null);

            List<ProductDto> product_requests = [product_request_1, product_request_2];

            ResponseDto persons_from_get = await _productService.GetAllProductsAsync();
            List<ProductDto> productList = persons_from_get.Result as List<ProductDto>;

            //Assert
            foreach (ProductDto product in productList)
            {
                Assert.Contains(product, product_requests);
            }
        }
        #endregion

    }
}
