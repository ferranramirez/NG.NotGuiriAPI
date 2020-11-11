using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class TourService : ITourService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public TourService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TourResponse Get(Guid id)
        {
            var tourWithDealTypes = _unitOfWork.Tour.GetWithDealTypes(id);

            return TourToTourResponse(tourWithDealTypes);
        }

        private static TourResponse TourToTourResponse((Tour, IEnumerable<DealType>) tourWithDealTypes)
        {
            TourResponse tour = (TourResponse)tourWithDealTypes.Item1;
            tour.DealTypes = tourWithDealTypes.Item2.ToList();
            return tour;
        }

        public async Task<IEnumerable<TourResponse>> GetFeatured()
        {
            var toursWithDealType = await _unitOfWork.Tour.GetFeatured();

            var tours = new List<TourResponse>();

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourResponse>> GetLastOnesCreated()
        {
            var toursWithDealType = await _unitOfWork.Tour.GetLastOnesCreated(5);

            var tours = new List<TourResponse>();

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourResponse>> GetByFullTag(string fullTag)
        {
            var toursWithDealType = await _unitOfWork.Tour.GetByFullTag(fullTag);

            var tours = new List<TourResponse>();

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourResponse>> GetByTag(string filter)
        {
            var toursWithDealType = await _unitOfWork.Tour.GetByTag(filter);

            var tours = new List<TourResponse>();

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourResponse>> GetByTagOrName(string filter)
        {
            var tours = new List<TourResponse>();

            IEnumerable<(Tour, IEnumerable<DealType>)> toursWithDealType;

            if (filter == null)
            {
                toursWithDealType = await _unitOfWork.Tour.GetAllWithDealTypes();

                toursWithDealType
                    .ToList()
                    .ForEach(t => tours.Add(TourToTourResponse(t)));

                return tours.Where(t => t.IsActive);
            }

            toursWithDealType = await _unitOfWork.Tour.GetByTagOrName(filter);

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourResponse>> GetByCommerceName(string commerceName)
        {
            var tours = new List<TourResponse>();

            IEnumerable<(Tour, IEnumerable<DealType>)> toursWithDealType;

            if (commerceName == null)
            {
                toursWithDealType = await _unitOfWork.Tour.GetAllWithDealTypes();

                toursWithDealType
                    .ToList()
                    .ForEach(t => tours.Add(TourToTourResponse(t)));

                return tours.Where(t => t.IsActive);
            }

            toursWithDealType = await _unitOfWork.Tour.GetByCommerceName(commerceName);

            toursWithDealType
                .ToList()
                .ForEach(t => tours.Add(TourToTourResponse(t)));

            return tours;
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDealType(string dealType)
        {
            //if (dealType == null)
            //{
            //    toursWithDealType = await _unitOfWork.Tour.GetAllWithDealTypes();
            //    return tours.Where(t => t.IsActive);
            //}

            return await _unitOfWork.Tour.GetByDealType(dealType);
        }
    }
}
