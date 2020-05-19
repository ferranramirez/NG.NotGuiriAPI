using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class CouponService : ICouponService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public CouponService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Add(Coupon coupon)
        {
            coupon.Id = Guid.NewGuid();
            _unitOfWork.Repository<Coupon>().Add(coupon);
            return _unitOfWork.Commit() == 1;
        }
    }
}
