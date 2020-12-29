using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.NotGuiriAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ITourService
    {
        TourWithDealType Get(Guid id);
        Task<IEnumerable<TourWithDealType>> GetFeatured(int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetLastOnesCreated(int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByTag(string filter, int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter, int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByCommerceName(string commerceName, int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByDealType(string dealType, int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByEverything(string filter, int? pageNumber, int? pageSize);
        Task<IEnumerable<TourWithDealType>> GetByDistance(LocationRequest location, int? pageNumber, int? pageSize);
    }
}
