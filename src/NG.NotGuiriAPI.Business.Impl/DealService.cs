using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class DealService : IDealService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public DealService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Deal>> GetAll()
        {
            return await _unitOfWork.Repository<Deal>().GetAll();
        }
    }
}
