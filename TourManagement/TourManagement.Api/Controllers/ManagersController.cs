using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagement.Api.DTOs;
using TourManagement.Api.Repositories;

namespace TourManagement.Api.Controllers
{
    [Route("api/managers")]
    public class ManagersController : ControllerBase
    {
        private readonly ITourRepository _tourRepository;

        public ManagersController(ITourRepository tourManagementRepository)
        {
            _tourRepository = tourManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            var managersFromRepo = await _tourRepository.GetManagers();

            var managers = Mapper.Map<IEnumerable<ManagerDto>>(managersFromRepo);

            return Ok(managers);
        }
    }
}
