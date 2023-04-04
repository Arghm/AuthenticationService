using AuthenticationService.Api;
using AuthenticationService.Contracts.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationService.Tests.ServicesTests.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _webFactory = null!;

        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _webFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        public async Task CheckStatus_Login_ShouldReturnOk(string url)
        {
            // Arrange
            var client = _webFactory.CreateClient();
            LoginModel login = new LoginModel { UserName = "User1", Password = "1" };

            // Act
            JsonContent content = JsonContent.Create(login);
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
