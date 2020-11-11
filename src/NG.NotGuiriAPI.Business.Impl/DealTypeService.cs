using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class DealTypeService : IReadAllService<DealType>
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public DealTypeService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DealType>> GetAll()
        {
            return await _unitOfWork.Repository<DealType>().GetAll();
        }
    }
}
