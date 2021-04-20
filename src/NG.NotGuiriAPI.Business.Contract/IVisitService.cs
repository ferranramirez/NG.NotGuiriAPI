using System;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface IVisitService
    {
        Task<Guid> Add(Guid userId, Guid commerceId, Guid tourId);
    }
}
