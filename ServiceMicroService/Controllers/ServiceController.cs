using IdentityMicroService.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceMicroService.Application.DTO.Service;
using ServiceMicroService.Application.Services.Abstractions;

namespace ServiceMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var services = await _serviceService.GetAsync(true);
            return Ok(services);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAsync(true);
            return Ok(services);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var service = await _serviceService.GetByIDAsync(id);
            if (service == null)
                return NotFound();
            return Ok(service);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceForCreatedDTO model)
        {
            var service = await _serviceService.CreateAsync(model);
            if (service == null)
                return BadRequest();
            return Created("", service);
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ServiceForUpdateDTO model)
        {
            var service = await _serviceService.UpdateAsync(id, model);
            if (service == null)
                return NotFound();

            return NoContent();
        }

        [Authorize(Roles = nameof(UserRole.Receptionist))]
        [HttpPut("{id}/status/{status}")]
        public async Task<IActionResult> UpdateStatus(string id, bool status)
        {
            var service = await _serviceService.ChangeStatusAsync(id, status);
            if (service == null)
                return NotFound();

            return NoContent();
        }
    }
}