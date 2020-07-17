using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ICouponService
    {
        Task<Coupon> Add(Guid userId, Guid nodeId, string content);
    }
}
