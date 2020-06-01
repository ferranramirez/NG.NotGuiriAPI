using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface INodeService
    {
        Node Get(Guid id);

        IEnumerable<Node> GetNodes(Guid tourId);
    }
}
