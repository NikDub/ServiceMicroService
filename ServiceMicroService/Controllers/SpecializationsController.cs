using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceMicroService.Application.DTO.Specialization;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Enums;

namespace ServiceMicroService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationsController : Controller
{
    private readonly ISpecializationService _specializationService;

    public SpecializationsController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Get()
    {
        var services = await _specializationService.GetAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetById(string id)
    {
        var service = await _specializationService.GetByIdAsync(id);
        if (service == null)
            return NotFound();
        return Ok(service);
    }

    [HttpGet("{id}/service")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetByIdWithServices(string id)
    {
        var service = await _specializationService.GetByIdWithServicesAsync(id);
        if (service == null)
            return NotFound();
        return Ok(service);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Create([FromBody] SpecializationForCreatedDto model)
    {
        var service = await _specializationService.CreateAsync(model);
        if (service == null)
            return BadRequest("Something went wrong");
        return Created("", service);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Update(string id, [FromBody] SpecializationForUpdateDto model)
    {
        var service = await _specializationService.UpdateAsync(id, model);
        if (service == null)
            return NotFound();
        return NoContent();
    }

    [HttpPut("{id}/status/{status}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> UpdateStatus(string id, bool status)
    {
        var service = await _specializationService.ChangeStatusAsync(id, status);
        if (service == null)
            return NotFound();
        return NoContent();
    }
}