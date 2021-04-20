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
    public class VisitService : IVisitService
    {
        public readonly IAPIUnitOfWork _unitOfWork;
        private readonly Dictionary<BusinessErrorType, BusinessErrorObject> _errors;

        public VisitService(IAPIUnitOfWork unitOfWork,
            IOptions<Dictionary<BusinessErrorType, BusinessErrorObject>> errors)
        {
            _unitOfWork = unitOfWork;
            _errors = errors.Value;
        }

        public async Task<Guid> Add(Guid userId, Guid commerceId, Guid tourId)
        {
            var tour = _unitOfWork.Tour.Get(tourId);
            var commerce = _unitOfWork.Commerce.Get(commerceId);

            var isCommerceInTour = (tour != null) && tour.Nodes.Any(n => n.LocationId == commerce?.LocationId);

            if (!isCommerceInTour)
            {
                var error = _errors[BusinessErrorType.TourWithoutCommerce];
                throw new NotGuiriBusinessException(error.Message, error.ErrorCode);
            }

            var visit = new Visit()
            {
                Id = new Guid(),
                UserId = userId,
                CommerceId = commerceId,
                TourId = tourId,
                RegistryDate = DateTime.Now
            };

            _unitOfWork.Repository<Visit>().Add(visit);
            await _unitOfWork.CommitAsync();

            return visit.Id;
        }
    }
}
