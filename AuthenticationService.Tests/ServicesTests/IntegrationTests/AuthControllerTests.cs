using AuthenticationService.Api;
using AuthenticationService.Contracts.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Tests.ServicesTests.IntegrationTests
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        public async Task CheckStatus_Login_ShouldReturnOk()
        {
            // Arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = new HttpClient();
            LoginModel login = new LoginModel { UserName = "User1", Password = "1" };

            // Act
            JsonContent content = JsonContent.Create(login);
            HttpResponseMessage response = await httpClient.PostAsync("api/auth/login", content);

            // Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
