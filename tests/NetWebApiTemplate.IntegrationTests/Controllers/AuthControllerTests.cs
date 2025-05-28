using NetWebApiTemplate.Api.Models;
using System.Net;
using System.Net.Http.Json;

namespace NetWebApiTemplate.IntegrationTests.Controllers
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_Login_Should_Return_Success()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync("/auth/login", new LoginRequest
            {
                Username = "sysadmin",
                Password = "SysAdmin123!",
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_Login_Should_Return_UsernameOrPasswordIncorrect()
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsJsonAsync("/auth/login", new LoginRequest
            {
                Username = "sysadmin",
                Password = "12345",
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}