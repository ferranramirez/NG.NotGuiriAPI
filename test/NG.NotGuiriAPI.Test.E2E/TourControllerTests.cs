using Newtonsoft.Json;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Test.E2E.Fixture;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NG.NotGuiriAPI.Test.E2E
{
    public class TourControllerTests : IClassFixture<HttpUtilities>
    {
        public HttpUtilities _httpUtilities;
        public TourControllerTests(HttpUtilities httpUtilities)
        {
            _httpUtilities = httpUtilities;
        }

        [Fact]
        public async Task DoingAGetRequestToFeaturedTours_ShouldReturnToursAsJson()
        {
            // Arrange
            var client = _httpUtilities.HttpClient;

            // Act
            var httpResponse = await client.GetAsync("/Tour/GetFeatured");

            // Assert
            httpResponse.EnsureSuccessStatusCode();

            string response = await httpResponse.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            IEnumerable<Tour> model = JsonConvert.DeserializeObject<List<Tour>>(response);
            Assert.NotNull(model);
        }
    }
}
