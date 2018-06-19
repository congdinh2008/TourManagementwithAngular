using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.DTOs;
using TourManagement.Api.Helpers;
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

            var tours = Mapper.Map<IEnumerable<TourDto>>(toursFromRepo);

            return Ok(tours);
        }

        //[HttpGet("{tourId}", Name = "GetTour")]
        //public async Task<IActionResult> GetTour(Guid tourId)
        //{
        //    var tourFromRepo = await _tourRepository.GetTour(tourId);

        //    if (tourFromRepo == null)
        //    {
        //        return BadRequest();
        //    }

        //    var tour = Mapper.Map<TourDto>(tourFromRepo);

        //    return Ok(tour);
        //}

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] { "application/vnd.vivustore.tour+json"})]
        public async Task<IActionResult> GetTour(Guid tourId)
        {
            return await GetSpecificTour<TourDto>(tourId);
        }

        [HttpGet("{tourId}")]
        [RequestHeaderMatchesMediaType("Accept",
            new[] { "application/vnd.vivustore.tourwithestimatedprofits+json" })]
        public async Task<IActionResult> GetTourWithEstimatedProfits(Guid tourId)
        {
            return await GetSpecificTour<TourWithEstimatedProfitsDto>(tourId);
        }

        private async Task<IActionResult> GetSpecificTour<T>(Guid tourId) where T : class
        {
            var tourFromRepo = await _tourRepository.GetTour(tourId);

            if (tourFromRepo == null)
            {
                return BadRequest();
            }

            var tour = Mapper.Map<T>(tourFromRepo);

            return Ok(tour);
        }
    }
}
