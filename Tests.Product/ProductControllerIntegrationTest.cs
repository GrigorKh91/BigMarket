using FluentAssertions;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace Tests.Products
{
    public class ProductControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }


        #region Get

        [Fact]
        public async Task Get_ToReturnAllProduct()
        {
            //Arrange

            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/product");

            //Assert
            response.Should().BeSuccessful(); //2xx


            string responseBody = await response.Content.ReadAsStringAsync();

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(responseBody);
            var document = html.DocumentNode;

            document.QuerySelectorAll("table.products").Should().NotBeNull();
        }

        #endregion
    }
}
