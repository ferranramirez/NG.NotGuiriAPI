using NG.DBManager.Infrastructure.Contracts.Models;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ICouponService
    {
        bool Add(Coupon coupon);
    }
}
