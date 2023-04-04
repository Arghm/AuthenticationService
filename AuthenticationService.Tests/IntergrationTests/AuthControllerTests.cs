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

            Environment.SetEnvironmentVariable("JWT_ACCESS_TOKEN_KEY", "EB0C1985-DDEC-4276-87F4-91992927C064@accessTokenKey");
            Environment.SetEnvironmentVariable("JWT_REFRESH_TOKEN_KEY", "EB0C1985-DDEC-4276-87F4-91992927C064@refreshTokenKey");
        }

        [Theory]
        [InlineData("/api/auth/login")]
        public async Task CheckStatus_Login_ShouldReturnOk(string url)
        {
            // Arrange
            HttpClient? client = _webFactory.CreateClient();
            LoginModel login = new LoginModel { UserName = "User1", Password = "1" };

            // Act
            JsonContent content = JsonContent.Create(login);
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
