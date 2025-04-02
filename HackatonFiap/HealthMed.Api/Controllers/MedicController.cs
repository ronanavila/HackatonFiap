using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
  [AllowAnonymous]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateAppointment([FromBody] ScheduleCreationDto request)
  {    
    try
    {
      Guid medicUid;
      Guid.TryParse(HttpContext?.User?.Identity?.Name!, out medicUid);

      var result = await _medicService.CreateSchedule(request, medicUid);

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
  public async Task<IActionResult> EditAppointment([FromBody] ScheduleUpdateDto request)
  {
    try
    {
      Guid medicUid;
      Guid.TryParse(HttpContext?.User?.Identity?.Name!, out medicUid);
      var result = await _medicService.EditSchedule(request, medicUid);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }

  [HttpGet]
  [Route("get-appointment")]
  [Authorize(Roles = "medic")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAppointment()
  {
    try
    {
      Guid medicUid;
      Guid.TryParse(HttpContext?.User?.Identity?.Name!, out medicUid);
      var result = await _medicService.GetScheduleByMedicUid(medicUid);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }
}
