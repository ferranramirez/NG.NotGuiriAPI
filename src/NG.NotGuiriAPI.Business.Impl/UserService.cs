using NG.Common.Services.AuthorizationProvider;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Domain;
using System;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class UserService : IUserService
    {
        public readonly IAPIUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IAPIUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public UserResponse Get(Guid userId)
        {
            var user = _unitOfWork.User.Get(userId);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Birthdate = user.Birthdate,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Role = user.Role,
                ImageId = user.ImageId,
                Commerces = user.Commerces,
            };
        }

        public async Task<User> Edit(UpdateUserRequest updateUserRequest, Guid userId)
        {
            var password = (updateUserRequest.Password != null) ? _passwordHasher.Hash(updateUserRequest.Password) : null;
            var user = new User
            {
                Id = userId,
                Name = updateUserRequest.Name,
                Birthdate = updateUserRequest.Birthdate,
                PhoneNumber = updateUserRequest.PhoneNumber,
                Email = updateUserRequest.Email,
                Password = password
            };

            var updatedUser = _unitOfWork.User.Edit(user);
            await _unitOfWork.CommitAsync();

            return updatedUser;
        }
    }
}
