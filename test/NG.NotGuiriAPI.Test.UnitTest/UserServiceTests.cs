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
        private Mock<IAPIUnitOfWork> _unitOfWorkMock;
        private IUserService _userService;
        private Guid rightUserId;
        private User expectedUser;
        private Guid wrongUserId;
        private User wrongUser;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IAPIUnitOfWork>();
            _userService = new UserService(_unitOfWorkMock.Object);

            rightUserId = Guid.NewGuid();
            expectedUser = new User
            {
                Id = rightUserId,
                Name = "Steve",
                Surname = "Jobs",
                Email = "steve@jobs.com",
                Birthdate = DateTime.Parse("01/01/1970"),
                Role = Role.Premium
            };

            wrongUserId = Guid.NewGuid();
            wrongUser = new User
            {
                Id = wrongUserId,
                Name = "Bill",
                Surname = "Gates",
                Email = "bill@gates.com",
                Birthdate = DateTime.Parse("01/01/1970"),
                Role = Role.Premium
            };
        }

        [Fact]
        public void GetUser_GivesRightUser_ReturnsExpectedUser()
        {
            //Arrange
            _unitOfWorkMock.Setup(uow => uow.User.GetByEmail("steve@jobs.com")).Returns(expectedUser);
            _unitOfWorkMock.Setup(uow => uow.User.Get(rightUserId)).Returns(expectedUser);

            //Act
            var actual = _userService.Get(rightUserId);

            //Assert
            Assert.Equal("steve@jobs.com", actual.Email);
        }

        [Fact]
        public void GetUser_GivesWrongUserId_ReturnsNull()
        {
            //Arrange
            _unitOfWorkMock.Setup(uow => uow.User.GetByEmail("steve@jobs.com")).Returns(expectedUser);
            _unitOfWorkMock.Setup(uow => uow.User.Get(wrongUserId)).Returns(wrongUser);

            //Act
            var actual = _userService.Get(wrongUserId);

            //Assert
            Assert.NotEqual("steve@jobs.com", actual.Email);
        }
    }
}
