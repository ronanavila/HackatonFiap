using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Shared;

namespace HealthMed.Api.Controllers;
[Route("api/patient")]
[ApiController]
public class PatientController : Controller
{
  [HttpPost]
  [Route("patient")]
  [Authorize(Roles = "find-medics")]

  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public IActionResult FindMedics([FromQuery] string specialiy)
  {
    try
    {
      //var result = _loginService.GetLogin(request.Login, request.Password, "patient");

      return StatusCode(200, "result");
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }
}
