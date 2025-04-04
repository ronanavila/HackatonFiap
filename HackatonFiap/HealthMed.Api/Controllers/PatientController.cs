using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Shared;

namespace HealthMed.Api.Controllers;
[Route("api/patient")]
[ApiController]
public class PatientController : Controller
{
  private readonly IPatientService _patientService;

  public PatientController(IPatientService patienceService)
  {
    _patientService = patienceService;
  }

  [HttpGet]
  [Route("find-medics-by-specialty")]
  [Authorize(Roles = "patient")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> FindMedics([FromQuery(Name = "scpecialty")] string scpecialty)
  {
    try
    {
      var result = await _patientService.GetMedicBySpecialty(scpecialty);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao tentar buscar os médicos");
    }
  }

  [HttpGet]
  [Route("find-medic-schedule")]
  [Authorize(Roles = "patient")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> FindMedicSchedule([FromQuery(Name = "uid")] Guid uid)
  {
    try
    {
      var result = await _patientService.GetMedicSchedule(uid);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao tentar buscar os médicos");
    }
  }

  [HttpPost]
  [Route("book-appointment")]
  [Authorize(Roles = "patient")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> BookAppointment([FromBody] ScheduleAppointmentDto appointmentDto)
  {
    try
    {
      Guid patientUid;
      Guid.TryParse(HttpContext?.User?.Identity?.Name!, out patientUid);

      var result = await _patientService.BookAppointment(appointmentDto, patientUid);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao tentar agendar");
    }
  }

  [HttpPost]
  [Route("cancel-appointment")]
  [Authorize(Roles = "patient")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentDto cancelAppointmentDto)
  {
    try
    {
      Guid patientUid;
      Guid.TryParse(HttpContext?.User?.Identity?.Name!, out patientUid);

      var result = await _patientService.CancelAppointment(cancelAppointmentDto, patientUid);

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao tentar cancelar agendamento");
    }
  }
}
