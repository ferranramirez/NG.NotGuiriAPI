﻿using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Node> GetNodes(Guid id)
        {
            var tour = _unitOfWork.Tour.Get(id);
            return tour.Nodes;
        }

        public async Task<IEnumerable<Tour>> GetFeatured()
        {
            return await _unitOfWork.Tour.GetFeatured();
        }

        public async Task<IEnumerable<Tour>> GetLastOnesCreated()
        {
            return await _unitOfWork.Tour.GetLastOnesCreated(5);
        }

        public async Task<IEnumerable<Tour>> GetByFullTag(string fullTag)
        {
            return await _unitOfWork.Tour.GetByFullTag(fullTag);
        }

        public async Task<IEnumerable<Tour>> GetByTag(string filter)
        {
            return await _unitOfWork.Tour.GetByTag(filter);
        }

        public async Task<IEnumerable<Tour>> GetByTagOrName(string filter)
        {
            return await _unitOfWork.Tour.GetByTagOrName(filter);
        }

        public Tour Get(Guid id)
        {
            return _unitOfWork.Tour.Get(id);
        }
    }
}
