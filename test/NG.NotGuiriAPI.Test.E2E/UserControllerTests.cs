using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Test.E2E.Fixture;
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

            var Configuration = _httpUtilities.ServiceProvider.GetService<IConfiguration>();
            _authorizationProvider = ioCModule.BuildServiceProvider(Configuration).GetService<IAuthorizationProvider>();
        }

        [Fact]
        public async Task GetRequestToAuthorizedUser_ShouldReturnUserAsJson()
        {
            // ARRANGE
            var client = _httpUtilities.HttpClient;

            AuthorizedUser authUser = new AuthorizedUser("ferran@notguiri.com", "Admin");

            var token = _authorizationProvider.GetToken(authUser);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            // ACT
            var httpResponse = await client.GetAsync("/User");

            // ASSERT
            httpResponse.EnsureSuccessStatusCode();

            string response = await httpResponse.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            User model = JsonConvert.DeserializeObject<User>(response);
            Assert.NotNull(model);
        }
    }
}
