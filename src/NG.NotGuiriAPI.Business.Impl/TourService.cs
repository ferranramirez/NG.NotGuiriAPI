using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCoordinatePortable;

namespace NG.NotGuiriAPI.Business.Impl
{
    public class TourService : ITourService
    {
        public readonly IAPIUnitOfWork _unitOfWork;

        public TourService(IAPIUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TourWithDealType Get(Guid id)
        {
            return _unitOfWork.Tour.GetWithDealTypes(id);
        }

        public async Task<IEnumerable<TourWithDealType>> GetFeatured(int? pageNumber, int? pageSize)
        {
            return await _unitOfWork.Tour.GetFeatured(pageNumber, pageSize);
        }

        public async Task<IEnumerable<TourWithDealType>> GetLastOnesCreated(int? pageNumber, int? pageSize)
        {
            return await _unitOfWork.Tour.GetLastOnesCreated(5, pageNumber, pageSize);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByTag(string filter, int? pageNumber, int? pageSize)
        {
            if (filter == null)
                return await GetAllActiveTours(pageNumber, pageSize);

            return await _unitOfWork.Tour.GetByTag(filter);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter, int? pageNumber, int? pageSize)
        {
            if (filter == null)
                return await GetAllActiveTours(pageNumber, pageSize);

            return await _unitOfWork.Tour.GetByTagOrName(filter);
        }

        private async Task<IEnumerable<TourWithDealType>> GetAllActiveTours(int? pageNumber, int? pageSize)
        {
            var tours = await _unitOfWork.Tour.GetAllWithDealTypes(pageNumber, pageSize);
            return tours.Where(t => t.IsActive);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByCommerceName(string commerceName, int? pageNumber, int? pageSize)
        {
            if (commerceName == null)
                return await GetAllActiveTours(pageNumber, pageSize);

            return await _unitOfWork.Tour.GetByCommerceName(commerceName);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDealType(string dealType, int? pageNumber, int? pageSize)
        {
            if (dealType == null)
                return await GetAllActiveTours(pageNumber, pageSize);

            return await _unitOfWork.Tour.GetByDealType(dealType);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByEverything(string filter, int? pageNumber, int? pageSize)
        {
            if (filter == null)
                return await GetAllActiveTours(pageNumber, pageSize);

            return await _unitOfWork.Tour.GetByEverything(filter);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDistance(LocationRequest location, int? pageNumber, int? pageSize)
        {
            var tours = await _unitOfWork.Tour.GetAllWithDealTypesAndLocation(pageNumber, pageSize);

            var toursByDistance =
                tours.Where(tour =>
                {
                    Location nodeLocation = tour.Nodes.OrderBy(n => n.Order).First().Location;

                    return GetDistance(
                        new GeoCoordinate(location.Latitude, location.Longitude),
                        new GeoCoordinate((double)nodeLocation.Latitude, (double)nodeLocation.Longitude)) <= location.Radius;
                });

            return toursByDistance;
        }

        private double GetDistance(GeoCoordinate pin1, GeoCoordinate pin2)
        {
            return pin1.GetDistanceTo(pin2);
        }
    }
}
