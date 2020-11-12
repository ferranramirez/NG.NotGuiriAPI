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
        Task<IEnumerable<TourWithDealType>> GetFeatured();
        Task<IEnumerable<TourWithDealType>> GetLastOnesCreated();
        Task<IEnumerable<TourWithDealType>> GetByTag(string filter);
        Task<IEnumerable<TourWithDealType>> GetByTagOrName(string filter);
        Task<IEnumerable<TourWithDealType>> GetByCommerceName(string commerceName);
        Task<IEnumerable<TourWithDealType>> GetByDealType(string dealType);
        Task<IEnumerable<TourWithDealType>> GetByEverything(string filter);
    }
}
