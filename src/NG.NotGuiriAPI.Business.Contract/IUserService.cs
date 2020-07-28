using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Domain;
using System;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface IUserService
    {
        UserResponse Get(Guid userId);
        Task<User> Edit(UpdateUserRequest updateUserRequest, Guid userId);
    }
}
