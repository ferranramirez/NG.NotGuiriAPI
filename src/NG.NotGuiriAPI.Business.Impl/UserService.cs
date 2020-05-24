using NG.Common.Utilities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System.Linq;
using System.Security.Claims;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class UserService : IUserService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public UserService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Get(string authorizationHeader)
        {
            var tokenClaims = TokenUtilities.GetClaims(authorizationHeader);

            var emailAddress = tokenClaims.First(c => string.Equals(c.Type, ClaimTypes.Email)).Value;

            return _unitOfWork.User.GetByEmail(emailAddress);
        }
    }
}
