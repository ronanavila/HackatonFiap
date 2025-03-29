namespace HealthMed.Domain.Contratcs;
public interface ITokenService
{
  public string GenerateToken(string login, string password);
}
