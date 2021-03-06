using Moq;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Business.Impl;
using System.Threading.Tasks;
using Xunit;

namespace NG.NotGuiriAPI.Test.UnitTest
{
    public class TourServiceTests/* : IClassFixture<DatabaseUtilities>*/
    {
        //private readonly DatabaseUtilities _databaseUtilities;
        private Mock<IAPIUnitOfWork> _uowMock;
        private ITourService _tourService;

        public TourServiceTests(/*DatabaseUtilities databaseUtilities*/)
        {
            //_databaseUtilities = databaseUtilities;

            _uowMock = new Mock<IAPIUnitOfWork>();
            _tourService = new TourService(_uowMock.Object);
        }

        [Fact(Skip = "Doesn't work at the moment")]
        public void GetFeaturedToursTest()
        {
            ////Arrange            
            //IEnumerable<Tour> expected = new List<TourResponse>() { new TourResponse { Id = Guid.NewGuid() } };

            //_uowMock.Setup(uow => uow.Tour.GetFeatured()).Returns(Task.FromResult(expected));

            ////Act
            //var actual = await _tourService.GetFeatured();

            ////Assert
            //Assert.Equal(expected, actual);
        }
    }
}
