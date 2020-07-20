using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class NodeService : INodeService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public NodeService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Node Get(Guid id)
        {
            return _unitOfWork.Node.Get(id);
        }

        public async Task<IEnumerable<Node>> GetNodes(Guid tourId)
        {
            var nodes = await _unitOfWork.Repository<Node>().GetAll(n => n.Location);
            return nodes.Where(n => n.TourId == tourId);
        }
    }
}
