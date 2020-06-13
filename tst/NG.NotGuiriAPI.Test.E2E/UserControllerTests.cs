using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using NG.NotGuiriAPI.Test.E2E.Fixture;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace NG.NotGuiriAPI.Test.E2E
{
    public class UserControllerTests : IClassFixture<HttpUtilities>, IClassFixture<IoCModule>
    {
        public HttpUtilities _httpUtilities;
        public IAuthorizationProvider _authorizationProvider;
        public UserControllerTests(HttpUtilities httpUtilities, IoCModule ioCModule)
        {
            _httpUtilities = httpUtilities;

            var Configuration = _httpUtilities.ServiceProvider.GetRequiredService<IConfiguration>();
            _authorizationProvider = ioCModule.BuildServiceProvider(Configuration).GetRequiredService<IAuthorizationProvider>();
        }

        [Fact]
        public async Task GetRequestToAuthorizedUser_ShouldReturnUserAsJson()
        {
            // Arrange
            var client = _httpUtilities.HttpClient;

            Guid userId = Guid.Parse("115A81C7-E960-4B88-ACA9-496D80745253");
            AuthorizedUser authUser = new AuthorizedUser(userId, "basic@test.org", nameof(Role.Basic));

            var token = _authorizationProvider.GetToken(authUser);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            // Act
            var httpResponse = await client.GetAsync("/User");

            // Assert
            httpResponse.EnsureSuccessStatusCode();

            string response = await httpResponse.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            User model = JsonConvert.DeserializeObject<User>(response);
            Assert.NotNull(model);
        }
    }
}
