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
            var user = _unitOfWork.User.Get(userId);

            if (updateUserRequest.Name != null) user.Name = updateUserRequest.Name;
            if (updateUserRequest.Birthdate != null) user.Birthdate = updateUserRequest.Birthdate;
            if (updateUserRequest.PhoneNumber != null) user.PhoneNumber = updateUserRequest.PhoneNumber;
            if (updateUserRequest.Email != null) user.Email = updateUserRequest.Email;
            //if (updateUserRequest.Password != null) standardUser.Password = _passwordHasher.Hash(updateUserRequest.Password);

            _unitOfWork.User.Update(user);
            await _unitOfWork.CommitAsync();

            return _unitOfWork.User.Get(userId);
        }
    }
}
