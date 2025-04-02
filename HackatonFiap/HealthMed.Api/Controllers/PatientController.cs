using HealthMed.Application.Contracts;
using HealthMed.Application.Services;
using HealthMed.Domain.Entities;
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
}
