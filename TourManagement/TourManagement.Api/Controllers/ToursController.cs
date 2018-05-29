using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.Models;
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
            var toursFromRepo = await _tourRepository.GetTours();

           // var tours = Mapper.Map<IEnumerable<Tour>>(toursFromRepo);
            return Ok(toursFromRepo);
        }

        [HttpGet("{tourId}", Name ="GetTour")]
        public async Task<IActionResult> GetTour(Guid tourId)
        {
            var tourFromRepo = await _tourRepository.GetTour(tourId);

            if (tourFromRepo == null)
            {
                return BadRequest();
            }

            //var tour = Mapper.Map<Tour>(tourFromRepo);

            return Ok(tourFromRepo);
        }
    }
}
