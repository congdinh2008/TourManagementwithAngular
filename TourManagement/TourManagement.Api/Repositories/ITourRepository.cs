using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.Models;

namespace TourManagement.Api.Repositories
{
    public interface ITourRepository
    {
        Task AddShow(Guid tourId, Show show);
        Task AddTour(Tour tour);
        Task DeleteTour(Tour tour);
        Task<IEnumerable<Band>> GetBands();
        Task<IEnumerable<Manager>> GetManagers();
        Task<IEnumerable<Show>> GetShows(Guid tourId);
        Task<IEnumerable<Show>> GetShows(Guid tourId, IEnumerable<Guid> showIds);
        Task<Tour> GetTour(Guid tourId, bool includeShows = false);
        Task<IEnumerable<Tour>> GetTours(bool includeShows = false);
        Task<IEnumerable<Tour>> GetToursForManager(Guid managerId, bool includeShows = false);
        Task<bool> IsTourManager(Guid tourId, Guid managerId);
        Task<bool> SaveAsync();
        Task<bool> TourExists(Guid tourId);
        Task UpdateTour(Tour tour);
    }
}