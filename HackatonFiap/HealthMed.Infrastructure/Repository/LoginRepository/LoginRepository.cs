using Dapper;
using HealthMed.Domain.Contracts;
using System.Data;

namespace HealthMed.Infrastructure.Repository.LoginRepository;

public class LoginRepository : ILoginRepository
{

  private readonly IDbConnection _connection;

  public LoginRepository(IDbConnection connection)
  {
    _connection = connection;
  }

  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";
  public async Task<Guid> Get(string userName, string password, string role)
  {
    if (role == "patient")
    {
      try
      {
        var queySchedule = @"SELECT [UID] FROM [PATIENT] WHERE PASSWORD = @PASSWORD AND (CPF = @USERNAME OR EMAIL= @USERNAME);";

        return await _connection.QueryFirstOrDefaultAsync<Guid>(queySchedule, new
        { password, userName });
      }
      catch
      {
        return Guid.Empty;
      }
    }

    try
    {
      var queySchedule = @"SELECT [UID] FROM [MEDIC] WHERE PASSWORD = @PASSWORD AND CRM = @USERNAME;";

      return await _connection.QueryFirstOrDefaultAsync<Guid>(queySchedule, new
      { password, userName });
    }
    catch
    {
      return Guid.Empty;
    }
  }
}

