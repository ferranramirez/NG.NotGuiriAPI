using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface IUserService
    {
        User Get(string authorizationHeader);
    }
}
