using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ICouponService
    {
        System.Threading.Tasks.Task<bool> Add(Coupon coupon);
    }
}
