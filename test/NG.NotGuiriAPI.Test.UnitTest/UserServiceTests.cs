using Moq;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Business.Impl;
using System;
using Xunit;

namespace NG.NotGuiriAPI.Test.UnitTest
{
    public class UserServiceTests
    {
        private Mock<IAPIUnitOfWork> _uowMock;
        private IUserService _userService;
        private User expected;

        public UserServiceTests()
        {
            _uowMock = new Mock<IAPIUnitOfWork>();
            _userService = new UserService(_uowMock.Object);

            expected = new User
            {
                Id = Guid.NewGuid(),
                Name = "Steve",
                Surname = "Jobs",
                Email = "steve@jobs.com",
                Role = Role.Premium
            };
        }

        [Fact]
        public void GetUserTest()
        {
            //Arrange
            // This token contains "steve@jobs.com" as Email
            string authorizationHeader = "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzdGV2ZUBqb2JzLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJQcmVtaXVtIn0.Gc-M_WCR0J-ZNeAe5V_oEhXkySTPSpvc825UWTI_bDQ";

            _uowMock.Setup(uow => uow.User.GetByEmail("steve@jobs.com")).Returns(expected);

            //Act
            var actual = _userService.Get(authorizationHeader);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUser_GivesWrongAuthHeader_ReturnsNull()
        {
            //Arrange
            // The email of this token is not "steve@jobs.com"
            string authorizationHeader = "bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzdGV2ZUB3b25kZXIuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlByZW1pdW0ifQ.3UAD0c4e17TxyfdUnaERE1RmXbf25TAL0nIHlsWlNaA";

            _uowMock.Setup(uow => uow.User.GetByEmail("steve@jobs.com")).Returns(expected);

            //Act
            var actual = _userService.Get(authorizationHeader);

            //Assert
            Assert.NotEqual(expected, actual);
        }
    }
}
