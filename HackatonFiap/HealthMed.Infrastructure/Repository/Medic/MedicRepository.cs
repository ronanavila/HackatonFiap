using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HealthMed.Infrastructure.Repository.Medic;
public class MedicRepository : IMedicRepository
{

  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

  public async Task<int> CreateSchedule(Schedule schedule)
  {
    int rows = 0;
    try
    {

      var insertSql = @"INSERT INTO [SCHEDULE]
        ([STARTSAT], [ENDSAT], [PRICE], [MEDICCRM])
        VALUES
        (@STARTSAT, @ENDSAT, @PRICE, @MEDICCRM)
    ;";

      using (var connection = new SqlConnection(connString))
      {
        return rows = await connection.ExecuteAsync(insertSql, new
        {
          schedule.StartsAt,
          schedule.EndsAt,
          schedule.Price,
          schedule.MedicCrm
        });
      }
    }
    catch
    {
      return rows;
    }
  }

  public async Task<int> EditSchedule(Schedule schedule)
  {
    int rows = 0;
    try
    {

      var updateSql = @"UPDATE [SCHEDULE]
        SET [STARTSAT] = @STARTSAT, [ENDSAT] = @ENDSAT, [PRICE] = @PRICE
        WHERE ID = @ID;";

      using (var connection = new SqlConnection(connString))
      {
        return rows = await connection.ExecuteAsync(updateSql, new
        {
          schedule.StartsAt,
          schedule.EndsAt,
          schedule.Price,
          schedule.Id
        });
      }
    }
    catch
    {
      return rows;
    }
  }

  public async Task<IEnumerable<Schedule>> GetScheduleByCrm(string crm)
  { 
    try
    {
      var queySchedule = @"SELECT 
        [ID]
        ,[STARTSAT]
        ,[ENDSAT]
        ,[PRICE]
        ,[APPROVED]
        ,[MEDICCRM]
        ,[PATIENTCPF]
      FROM [SCHEDULE] WHERE MEDICCRM = @CRM;";

      using (var connection = new SqlConnection(connString))
      {
        return  await connection.QueryAsync<Schedule>(queySchedule, new
        {crm});
        

      }
    }
    catch
    {
      return new List<Schedule>();
    }
  }
}
