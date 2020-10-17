using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class TagService : ITagService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public TagService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _unitOfWork.Repository<Tag>().GetAll();
        }
    }
}
