using Flunt.Notifications;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Contratcs;
using System.Net;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;

namespace HealthMed.Application.Services;
public class LoginService : ILoginService
{
  private readonly ITokenService _loginService;
  private readonly ILoginRepository _loginRepository;

  public LoginService(ITokenService loginService, ILoginRepository loginRepository)
  {
    _loginService = loginService;
    _loginRepository = loginRepository;
  }

  public async Task<IResponse> GetLogin(string login, string password, string role)
  {
    try
    {
      var result = await _loginRepository.Get(login, password, role);

      if (result == null)
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, new List<Notification>() { new Notification("Login", "Usuário ou senha inválidos") });
      }

      var token = _loginService.GenerateToken(login, role);

      return new BaseResponse(HttpStatusCode.OK, true,"Token gerado com sucesso.", token);
    }
    catch
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, new List<Notification>() { new Notification("Login", "Erro ao tentar gerar token") });

    }
  }
}
