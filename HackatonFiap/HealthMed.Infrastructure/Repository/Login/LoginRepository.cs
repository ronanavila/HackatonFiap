using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HealthMed.Infrastructure.Repository.Login;

public class LoginRepository : ILoginRepository
{
  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";
  public async Task<string> Get(string userName, string password, string role)
  {

    var patientLogin = new PatientLogin { EmailCpf = "patient", Password = "Jovem" };
    var medicLogin = new MedicLogin { Crm = "medic", Password = "Jovem" };


    if (role == "patient")
    {
      try
      {
        var queySchedule = @"SELECT [CPF] FROM [PATIENT] WHERE PASSWORD = @PASSWORD AND (CPF = @USERNAME OR EMAIL= @USERNAME);";

        using (var connection = new SqlConnection(connString))
        {
          return await connection.QueryFirstOrDefaultAsync<string>(queySchedule, new
          { password, userName }) ?? string.Empty;
        }
      }
      catch
      {
        return string.Empty;
      }
    }

    try
    {
      var queySchedule = @"SELECT [CRM] FROM [MEDIC] WHERE PASSWORD = @PASSWORD AND CRM = @USERNAME;";

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryFirstOrDefaultAsync<string>(queySchedule, new
        { password, userName }) ?? string.Empty;
      }
    }
    catch
    {
      return string.Empty;
    }
  }
}

