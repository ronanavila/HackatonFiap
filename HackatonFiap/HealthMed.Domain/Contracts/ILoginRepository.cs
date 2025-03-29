namespace HealthMed.Domain.Contracts;
public interface ILoginRepository
{
  public Task<object> Get(string userName, string password, string role);
}
