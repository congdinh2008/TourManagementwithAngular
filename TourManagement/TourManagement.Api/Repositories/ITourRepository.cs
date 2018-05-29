using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.Models;

namespace TourManagement.Api.Repositories
{
    public interface ITourRepository
    {
        Task<Tour> GetTour(Guid tourId);
        Task<IEnumerable<Tour>> GetTours();
    }
}