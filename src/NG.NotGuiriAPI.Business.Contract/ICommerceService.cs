using NG.DBManager.Infrastructure.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ICommerceService
    {
        Task<IEnumerable<Commerce>> GetAll();
    }
}
