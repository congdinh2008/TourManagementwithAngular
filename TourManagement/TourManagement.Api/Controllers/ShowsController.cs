using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.DTOs;
using TourManagement.Api.Repositories;

namespace TourManagement.Api.Controllers
{
    [Route("api/tours/{tourId}/shows")]
    public class ShowsController : ControllerBase
    {
        private readonly ITourRepository _tourRepository;

        public ShowsController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetShows(Guid tourId)
        {
            var tourFromRepo = await _tourRepository.GetTour(tourId, true);

            if (!(await _tourRepository.TourExists(tourId)))
            {
                return NotFound();
            }

            var showsFromRepo = await _tourRepository.GetShows(tourId);

            var shows = Mapper.Map<IEnumerable<ShowDto>>(showsFromRepo);
            return Ok(shows);
        }
    }
}
