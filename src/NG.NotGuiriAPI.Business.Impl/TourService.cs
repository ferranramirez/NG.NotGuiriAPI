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

        public async Task<IEnumerable<TourWithDealType>> GetFeatured()
        {
            return await _unitOfWork.Tour.GetFeatured();
        }

        public async Task<IEnumerable<TourWithDealType>> GetLastOnesCreated()
        {
            return await _unitOfWork.Tour.GetLastOnesCreated(5);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByTag(string filter)
        {
            if (filter == null)
                return await GetAllActiveTours();

            return await _unitOfWork.Tour.GetByTag(filter);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter)
        {
            if (filter == null)
                return await GetAllActiveTours();

            return await _unitOfWork.Tour.GetByTagOrName(filter);
        }

        private async Task<IEnumerable<TourWithDealType>> GetAllActiveTours()
        {
            var tours = await _unitOfWork.Tour.GetAllWithDealTypes();
            return tours.Where(t => t.IsActive);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByCommerceName(string commerceName)
        {
            if (commerceName == null)
                return await GetAllActiveTours();

            return await _unitOfWork.Tour.GetByCommerceName(commerceName);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDealType(string dealType)
        {
            if (dealType == null)
                return await GetAllActiveTours();

            return await _unitOfWork.Tour.GetByDealType(dealType);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByEverything(string filter)
        {
            if (filter == null)
                return await GetAllActiveTours();

            return await _unitOfWork.Tour.GetByEverything(filter);
        }

        public async Task<IEnumerable<TourWithDealType>> GetByDistance(LocationRequest location)
        {
            var tours = await _unitOfWork.Tour.GetAllWithDealTypesAndLocation();

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
