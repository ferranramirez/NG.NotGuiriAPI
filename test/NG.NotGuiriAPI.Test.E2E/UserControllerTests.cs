using Newtonsoft.Json;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Test.E2E.Fixture;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NG.NotGuiriAPI.Test.E2E
{
    public class UserControllerTests : IClassFixture<HttpUtilities>
    {
        public HttpUtilities _httpUtilities;
        public UserControllerTests(HttpUtilities httpUtilities)
        {
            _httpUtilities = httpUtilities;
        }

        [Fact]
        public async Task GetRequestToAuthorizedUser_ShouldReturnUserAsJson()
        {
            // ARRANGE
            var client = _httpUtilities.HttpClient;

            // ACT
            var httpResponse = await client.GetAsync("/User");

            // ASSERT
            httpResponse.EnsureSuccessStatusCode();

            string response = await httpResponse.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            IEnumerable<Tour> model = JsonConvert.DeserializeObject<List<Tour>>(response);
            Assert.NotNull(model);
        }
    }
}
