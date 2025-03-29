using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Shared;

namespace HealthMed.Api.Controllers;
[Route("api/medic")]
[ApiController]
public class MedicController : Controller
{
  [HttpPost]
  [Route("medic")]
  [Authorize(Roles = "medic")]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public IActionResult Medic([FromBody] PatientLogin request)
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
