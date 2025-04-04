using TechChallenge.Domain.Contracts;

namespace HealthMed.Domain.Contratcs;
public interface ILoginService
{
  public Task<IResponse> GetLogin(string login, string password, string role);
}
