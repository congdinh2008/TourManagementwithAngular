using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourManagement.Api.Data;
using TourManagement.Api.Models;

namespace TourManagement.Api.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ApplicationDbContext _context;

        public TourRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tour>> GetTours()
        {
            return await _context.Tours.ToListAsync();
        }

        public async Task<Tour> GetTour(Guid tourId)
        {
            return await _context.Tours
                .Where(t => t.TourId == tourId)
                .FirstOrDefaultAsync();
        }
    }
}
