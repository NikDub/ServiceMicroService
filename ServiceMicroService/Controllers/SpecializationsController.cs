using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceMicroService.Application.Dto.Specialization;
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
    [Authorize(Roles = $"{nameof(UserRole.Receptionist)},{nameof(UserRole.Patient)}")]
    public async Task<IActionResult> Get()
    {
        var specializations = await _specializationService.GetAsync();
        return Ok(specializations);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetById(Guid id)
    {
        var specializations = await _specializationService.GetByIdAsync(id);
        if (specializations == null)
            return NotFound($"The record {id} was not found.");
        return Ok(specializations);
    }

    [HttpGet("{id}/service")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetByIdWithServices(Guid id)
    {
        var specializations = await _specializationService.GetByIdWithServicesAsync(id);
        if (specializations == null)
            return NotFound($"The record {id} was not found.");
        return Ok(specializations);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Create([FromBody] SpecializationForCreatedDto model)
    {
        var specializations = await _specializationService.CreateAsync(model);
        if (specializations == null)
            return BadRequest("Something went wrong.");
        return Created("", specializations);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Update(Guid id, [FromBody] SpecializationForUpdateDto model)
    {
        var specializations = await _specializationService.UpdateAsync(id, model);
        if (specializations == null)
            return NotFound($"The record {id} was not found.");
        return NoContent();
    }

    [HttpPut("{id}/status/{status}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> UpdateStatus(Guid id, bool status)
    {
        var specializations = await _specializationService.ChangeStatusAsync(id, status);
        if (specializations == null)
            return NotFound($"The record {id} was not found.");
        return NoContent();
    }
}