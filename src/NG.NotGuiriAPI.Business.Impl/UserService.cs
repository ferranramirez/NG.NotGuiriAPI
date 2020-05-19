using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class UserService : IUserService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public UserService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Get(Guid id)
        {
            return _unitOfWork.Repository<User>().Get(id);
        }
    }
}
