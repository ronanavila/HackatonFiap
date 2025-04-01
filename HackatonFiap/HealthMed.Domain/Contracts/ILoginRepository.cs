namespace HealthMed.Domain.Contracts;
public interface ILoginRepository
{
  public Task<string> Get(string userName, string password, string role);
}
