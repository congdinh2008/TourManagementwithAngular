using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.DTOs;
using TourManagement.Api.Repositories;

namespace TourManagement.Api.Controllers
{
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly ITourRepository _tourRepository;

        public BandsController(ITourRepository tourManagementRepository)
        {
            _tourRepository = tourManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBands()
        {
            var bandsFromRepo = await _tourRepository.GetBands();

            var bands = Mapper.Map<IEnumerable<BandDto>>(bandsFromRepo);

            return Ok(bands);
        }
    }
}
