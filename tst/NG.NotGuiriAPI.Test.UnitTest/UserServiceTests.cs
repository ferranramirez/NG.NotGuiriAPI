using Moq;
using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Business.Impl;
using NG.NotGuiriAPI.Domain;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NG.NotGuiriAPI.Test.UnitTest
{
    public class UserServiceTests
    {
        private Mock<IAPIUnitOfWork> _unitOfWorkMock;
        private Mock<IPasswordHasher> _passwordHasher;
        private IUserService _userService;
        private Guid rightUserId;
        private User expectedUser;
        private Guid wrongUserId;
        private User wrongUser;

        public UserServiceTests()
        {
            _unitOfWorkMock = new Mock<IAPIUnitOfWork>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _userService = new UserService(_unitOfWorkMock.Object, _passwordHasher.Object);

            rightUserId = Guid.NewGuid();
            expectedUser = new User
            {
                Id = rightUserId,
                Name = "Steve Jobs",
                Email = "steve@jobs.com",
                Birthdate = DateTime.Parse("01/01/1970"),
                Role = Role.Premium
            };

            wrongUserId = Guid.NewGuid();
            wrongUser = new User
            {
                Id = wrongUserId,
                Name = "Bill Gates",
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

        [Fact]
        public async Task EditUser_GivesUpdateUserDetails_ReturnsModifiedUser()
        {
            //Arrange
            var updateUser = new UpdateUserRequest
            {
                Email = "updated@mail.com",
                Name = "updated",
                Birthdate = DateTime.Now.AddYears(-18),
                PhoneNumber = "+0000000000",
                Password = "updated"
            };

            var expected = new User
            {
                Id = rightUserId,
                Email = "updated@mail.com",
                Name = "updated",
                Birthdate = updateUser.Birthdate,
                PhoneNumber = "+0000000000",
                Password = "updated"
            };

            _unitOfWorkMock.Setup(uow => uow.User.Edit(expected)).Returns(expected);

            //Act
            var actual = await _userService.Edit(updateUser, rightUserId);

            //Assert
            Assert.Equal(actual, expected);
        }
    }
}
