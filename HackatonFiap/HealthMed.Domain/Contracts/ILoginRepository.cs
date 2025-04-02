namespace HealthMed.Domain.Contracts;
public interface ILoginRepository
{
  public Task<Guid> Get(string userName, string password, string role);
}
