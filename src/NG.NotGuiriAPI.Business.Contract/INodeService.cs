using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface INodeService
    {
        Node Get(Guid id);

        Task<IEnumerable<Node>> GetNodes(Guid tourId);
    }
}
