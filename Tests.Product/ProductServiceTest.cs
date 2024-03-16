using BigMarket.Services.ProductAPI.Services.IServices;
using ProductAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BigMarket.Services.ProductAPI.Services;
using BigMarket.Services.ProductAPI.Core.Models;
using EntityFrameworkCoreMock;
using BigMarket.Services.ProductAPI.Core.Models.Dto;
using AutoMapper;
using AutoFixture;
using ProductAPI.Core.Services.IServices;
using ProductAPI.Infrastructure.Repositories;

namespace Tests.Products
{
    public class ProductServiceTest
    {
        //private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
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

            _productRepository = new ProductRepository(dbContex);
        }


        #region AddProduct

        [Fact]
        public async Task AddProduct_NullProduct()
        {
            //Arrange
            Product productAddRequest = null;

            //Act
            Product response_from_create = await _productRepository.CreateProductAsync(productAddRequest);

            //Assert
            Assert.True(response_from_create != null);
        }

        [Fact]
        public async Task AddProduct_ProperProductDetails()
        {
            //Arrange
            Product productAddRequest = new()
            {
                Name = "BMW x5",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 100_000
            };

            //Act
            Product response_from_create = await _productRepository.CreateProductAsync(productAddRequest);

            var productList = await _productRepository.GetAllProductsAsync();
            //Assert
            Assert.Contains(productAddRequest, productList);
        }

        #endregion


        #region GetPersonByPersonID

        [Fact]
        public async Task GetProductByID_NullProductID()
        {
            //Arrange
            int productId = default;

            //Act
            Product product_response_from_get = await _productRepository.GetProductByIdAsync(productId);

            //Assert
            Assert.Null(product_response_from_get);
        }


        [Fact]
        public async Task GetProductByID_WithNewProductID()
        {
            //Arange
            Product productAddRequest = new()
            {
                Name = "BMW x5",
                CategoryName = "Super Car",
                Description = "go fast",
                Price = 100_000
            };

            //Product response_from_create = await _productRepository.CreateProductAsync(productAddRequest);

            //ResponseDto response_from_get = await _productService.GetProductByIdAsync(productDto.ProductId);
            //ProductDto product_response_from_get = response_from_get.Result as ProductDto;

            ////Assert
            //Assert.Equal(productDtoAddRequest, product_response_from_get);
        }

        #endregion


        #region GetAllProducts

        [Fact]
        public async Task GetAllProducts_EmptyList()
        {
            ////Act
            //ResponseDto persons_from_get = await _productService.GetAllProductsAsync();
            //List<ProductDto> productList = persons_from_get.Result as List<ProductDto>;

            ////Assert
            //Assert.Empty(productList);
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

            //await _productService.CreateProductAsync(product_request_1, null);
            //await _productService.CreateProductAsync(product_request_2, null);

            //List<ProductDto> product_requests = [product_request_1, product_request_2];

            //ResponseDto persons_from_get = await _productService.GetAllProductsAsync();
            //List<ProductDto> productList = persons_from_get.Result as List<ProductDto>;

            ////Assert
            //foreach (ProductDto product in productList)
            //{
            //    Assert.Contains(product, product_requests);
            //}
        }
        #endregion

    }
}
