using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class NodeService : INodeService
    {
        public readonly IUnitOfWork _unitOfWork;

        public NodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Node Get(Guid id)
        {
            return _unitOfWork.Repository<Node>().Get(id);
        }
    }
}
