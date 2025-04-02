namespace HealthMed.Domain.Contratcs;
public interface ITokenService
{
  public string GenerateToken(Guid guid, string password);
}
