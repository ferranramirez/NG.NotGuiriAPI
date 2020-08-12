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

        public async Task<Coupon> Add(Guid userId, Guid nodeId, string content)
        {
            DateTime time = DateTime.Now;

            var anyExistingCouponInLast3Hours = _unitOfWork.Coupon
                .Find(c => c.UserId == userId && c.NodeId == nodeId
                    && c.GenerationDate > time.AddHours(-3))
                .Any();

            if (anyExistingCouponInLast3Hours)
            {
                var error = _errors[BusinessErrorType.CouponNotAvailable];
                throw new NotGuiriBusinessException(error.Message, error.ErrorCode);
            }

            _unitOfWork.Coupon.InvalidatePastCoupons(userId, nodeId);
            Guid couponId = await NewCoupon(userId, nodeId, content, time);
            await _unitOfWork.CommitAsync();

            return _unitOfWork.Coupon.Get(couponId);
        }

        private async Task<Guid> NewCoupon(Guid userId, Guid nodeId, string content, DateTime time)
        {
            var coupon = new Coupon
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Content = content,
                NodeId = nodeId,
                ValidationDate = default,
                GenerationDate = time,
            };

            _unitOfWork.Coupon.Add(coupon);

            return coupon.Id;
        }

        public Coupon Get(Guid couponId)
        {
            return _unitOfWork.Coupon.Get(couponId);
        }
    }
}
