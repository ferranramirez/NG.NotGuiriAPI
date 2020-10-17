using NG.DBManager.Infrastructure.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Business.Contract
{
    public interface ITourService
    {
        Tour Get(Guid id);
        Task<IEnumerable<Tour>> GetFeatured();
        Task<IEnumerable<Tour>> GetLastOnesCreated();
        Task<IEnumerable<Tour>> GetByFullTag(string fullTag);
        Task<IEnumerable<Tour>> GetByTag(string filter);
        Task<IEnumerable<Tour>> GetByTagOrName(string filter);
        Task<IEnumerable<Tour>> GetByCommerceName(string commerceName);
    }
}
