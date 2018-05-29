using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourManagement.Api.Repositories;

namespace TourManagement.Api.Controllers
{
    [Route("api/tours")]
    public class ToursController : ControllerBase
    {
        private readonly ITourRepository _tourRepository;

        public ToursController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var tours = await _tourRepository.GetTours();

            return Ok(tours);
        }

        [HttpGet("{tourId}", Name ="GetTour")]
        public async Task<IActionResult> GetTour(Guid tourId)
        {
            var tour = await _tourRepository.GetTour(tourId);
            if (tour == null)
            {
                return BadRequest();
            }

            return Ok(tour);
        }
    }
}
