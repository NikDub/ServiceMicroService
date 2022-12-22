using IdentityMicroService.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Application.Services.Abstractions;

namespace ServiceMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var services = await _specializationService.GetAsync();
            return Ok(services);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var service = await _specializationService.GetByIdAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpGet("{id}/service")]
        public async Task<IActionResult> GetByIdWithServices(string id)
        {
            var service = await _specializationService.GetByIdWithServicesAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecializationForCreatedDTO model)
        {
            var service = await _specializationService.CreateAsync(model);
            if (service == null)
                return BadRequest();
            return Created("", service);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SpecializationForUpdateDTO model)
        {
            var service = await _specializationService.UpdateAsync(id, model);
            if (service == null)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPut("{id}/status/{status}")]
        public async Task<IActionResult> UpdateStatus(string id, bool status)
        {
            var service = await _specializationService.ChangeStatusAsync(id, status);
            if (service == null)
                return NotFound();

            return NoContent();
        }
    }
}
