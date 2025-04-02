using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace HealthMed.Infrastructure.Repository.MedicRepository;
public class MedicRepository : IMedicRepository
{

  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

  public async Task<int> CreateSchedule(Schedule schedule)
  {
    int rows = 0;
    try
    {

      var insertSql = @"INSERT INTO [SCHEDULE]
        ([STARTSAT], [ENDSAT], [PRICE], [MEDICUID])
        VALUES
        (@STARTSAT, @ENDSAT, @PRICE, @MEDICUID)
    ;";

      using (var connection = new SqlConnection(connString))
      {
        return rows = await connection.ExecuteAsync(insertSql, new
        {
          schedule.StartsAt,
          schedule.EndsAt,
          schedule.Price,
          schedule.MedicUID
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
        WHERE UID = @UID;";

      using (var connection = new SqlConnection(connString))
      {
        return rows = await connection.ExecuteAsync(updateSql, new
        {
          schedule.StartsAt,
          schedule.EndsAt,
          schedule.Price,
          schedule.UID
        });
      }
    }
    catch
    {
      return rows;
    }
  }

  public async Task<IEnumerable<Schedule>> GetScheduleByMedicUid(Guid guid)
  { 
    try
    {
      var queySchedule = @"SELECT 
        [UID]
        ,[STARTSAT]
        ,[ENDSAT]
        ,[PRICE]
        ,[APPROVED]
        ,[MEDICUID]
        ,[PATIENTUID]
      FROM [SCHEDULE] WHERE MEDICUID = @GUID;";

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryAsync<Schedule>(queySchedule,new
         { guid} );
        

      }
    }
    catch
    {
      return new List<Schedule>();
    }
  }
}
