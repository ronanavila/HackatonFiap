using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Shared;

namespace HealthMed.Api.Controllers;
[Route("api/medic")]
[ApiController]
public class MedicController : Controller
{

  private readonly IMedicService _medicService;

  public MedicController(IMedicService medicService)
  {
    _medicService = medicService;
  }

  [HttpPost]
  [Route("create-appointment")]
  [Authorize(Roles = "medic")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateAppointment([FromBody] ScheduleCreationDto request)
  {
    try
    {
      var result = await _medicService.CreateSchedule(request);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao criar agenda");
    }
  }

  [HttpPatch]
  [Route("edit-appointment")]
  [Authorize(Roles = "medic")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> EditAppointment([FromBody] ScheduleCreationDto request)
  {
    try
    {
      var result = await _medicService.EditSchedule(request);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }
}
