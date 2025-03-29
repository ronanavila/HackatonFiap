using HealthMed.Domain.Contratcs;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Domain.Shared;

namespace TechChallenge.ContactCreation.Controller.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
  private readonly ILoginService _loginService;

  public LoginController(ILoginService loginService)
  {
    _loginService = loginService;
  }


  /// <summary>
  /// Create a Contact.
  /// </summary>
  /// <returns>Return a Login Token</returns>
  /// <response code="200">Token created</response>
  /// <response code="400">If the parameters are wrong</response>
  /// <response code="500">Unexpected Error</response>
  [HttpPost]
  [Route("medic-login")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> MedicLogin([FromBody] MedicLogin request)
  {
    try
    {
      var  result = await _loginService.GetLogin(request.Crm, request.Password, "medic");

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }

  /// <summary>
  /// Create a Contact.
  /// </summary>
  /// <returns>Return a Login Token</returns>
  /// <response code="200">Token created</response>
  /// <response code="400">If the parameters are wrong</response>
  /// <response code="500">Unexpected Error</response>
  [HttpPost]
  [Route("patient-login")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> PatientLogin([FromBody] PatientLogin request)
  {
    try
    {
      var result = await _loginService.GetLogin(request.EmailCpf, request.Password, "patient");

      return StatusCode((int)result.StatusCode, result);
    }
    catch
    {
      return StatusCode(500, "Erro ao realizar o login");
    }
  }
}
