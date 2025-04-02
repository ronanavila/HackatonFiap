using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HealthMed.Infrastructure.Repository.LoginRepository;

public class LoginRepository : ILoginRepository
{
  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";
  public async Task<Guid> Get(string userName, string password, string role)
  {
    if (role == "patient")
    {
      try
      {
        var queySchedule = @"SELECT [UID] FROM [PATIENT] WHERE PASSWORD = @PASSWORD AND (CPF = @USERNAME OR EMAIL= @USERNAME);";

        using (var connection = new SqlConnection(connString))
        {
          return await connection.QueryFirstOrDefaultAsync<Guid>(queySchedule, new
          { password, userName });
        }
      }
      catch
      {
        return Guid.Empty;
      }
    }

    try
    {
      var queySchedule = @"SELECT [UID] FROM [MEDIC] WHERE PASSWORD = @PASSWORD AND CRM = @USERNAME;";

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryFirstOrDefaultAsync<Guid>(queySchedule, new
        { password, userName });
      }
    }
    catch
    {
      return Guid.Empty;
    }
  }
}

