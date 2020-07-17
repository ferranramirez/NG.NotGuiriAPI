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

        public User Get(Guid userId)
        {
            return _unitOfWork.User.Get(userId);
        }

        public async Task<User> Edit(UpdateUserRequest updateUserRequest, Guid userId)
        {
            var password = (updateUserRequest.Password != null) ? _passwordHasher.Hash(updateUserRequest.Password) : null;
            var user = new User
            {
                Id = userId,
                Name = updateUserRequest.Name,
                Surname = updateUserRequest.Surname,
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
