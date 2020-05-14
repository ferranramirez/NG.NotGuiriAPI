using NG.DBManager.Infrastructure.Contracts.Models;
using System;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface INodeService
    {
        Node Get(Guid id);
    }
}
