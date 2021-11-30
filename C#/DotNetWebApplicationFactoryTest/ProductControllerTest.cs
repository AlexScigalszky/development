using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Threading.Tasks;
using Xunit;

namespace Example.API.Test
{
    public class ProductsControllerTest
    {
        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        //[InlineData("/About")]
        //[InlineData("/Privacy")]
        //[InlineData("/Contact")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            await using var application = new PlaygroundApplication();

            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}