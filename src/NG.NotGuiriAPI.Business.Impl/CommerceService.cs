using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class CommerceService : IReadAllService<Commerce>
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public CommerceService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Commerce>> GetAll()
        {
            return await _unitOfWork.Commerce.GetAll();
        }
    }
}
