using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceMicroService.Application.Dto.Service;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Domain.Entities.Enums;

namespace ServiceMicroService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : Controller
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var services = await _serviceService.GetActiveAsync();
        return Ok(services);
    }

    [HttpGet("all")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceService.GetAllDividedByCategoryAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetById(string id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null)
            return NotFound($"The record {id} was not found.");
        return Ok(service);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Create([FromBody] ServiceForCreatedDto model)
    {
        var service = await _serviceService.CreateAsync(model);
        if (service == null)
            return BadRequest("Something went wrong.");
        return Created("", service);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Update(string id, [FromBody] ServiceForUpdateDto model)
    {
        var service = await _serviceService.UpdateAsync(id, model);
        if (service == null)
            return NotFound(
                $"Service {id} or category {model.CategoryName} or specialization {model.SpecializationName} was not found.");
        return NoContent();
    }

    [HttpPut("{id}/status/{status}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> UpdateStatus(string id, bool status)
    {
        var service = await _serviceService.ChangeStatusAsync(id, status);
        if (service == null)
            return NotFound($"The record {id} was not found.");
        return NoContent();
    }
}