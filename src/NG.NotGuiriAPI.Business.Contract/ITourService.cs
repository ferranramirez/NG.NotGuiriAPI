using NG.NotGuiriAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ITourService
    {
        TourResponse Get(Guid id);
        Task<IEnumerable<TourResponse>> GetFeatured();
        Task<IEnumerable<TourResponse>> GetLastOnesCreated();
        Task<IEnumerable<TourResponse>> GetByFullTag(string fullTag);
        Task<IEnumerable<TourResponse>> GetByTag(string filter);
        Task<IEnumerable<TourResponse>> GetByTagOrName(string filter);
        Task<IEnumerable<TourResponse>> GetByCommerceName(string commerceName);
    }
}
