using Microsoft.Extensions.Options;
using NG.Common.Library.Exceptions;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class CouponService : ICouponService
    {
        public readonly IAPIUnitOfWork _unitOfWork;
        private readonly Dictionary<BusinessErrorType, BusinessErrorObject> _errors;

        public CouponService(IAPIUnitOfWork unitOfWork,
            IOptions<Dictionary<BusinessErrorType, BusinessErrorObject>> errors)
        {
            _unitOfWork = unitOfWork;
            _errors = errors.Value;
        }

        public async Task<Coupon> Add(Guid userId, Guid commerceId, string content)
        {
            var couponId = Guid.NewGuid();
            var coupon = new Coupon
            {
                Id = couponId,
                UserId = userId,
                Content = content,
                CommerceId = commerceId,
                ValidationDate = default,
                GenerationDate = DateTime.Now,
            };

            var anyExistingCouponInLastMonth = _unitOfWork.Repository<Coupon>()
                .Find(c => c.UserId == userId && c.CommerceId == commerceId
                    && c.GenerationDate > coupon.GenerationDate.AddDays(-30))
                .Any();

            if (anyExistingCouponInLastMonth)
            {
                var error = _errors[BusinessErrorType.CouponNotAvailable];
                throw new NotGuiriBusinessException(error.Message, error.ErrorCode);
            }

            _unitOfWork.Repository<Coupon>().Add(coupon);
            await _unitOfWork.CommitAsync();

            return _unitOfWork.Repository<Coupon>().Get(couponId);
        }
    }
}
