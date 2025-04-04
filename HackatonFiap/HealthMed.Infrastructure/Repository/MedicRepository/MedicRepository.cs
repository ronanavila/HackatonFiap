using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using System.Data;

namespace HealthMed.Infrastructure.Repository.MedicRepository;
public class MedicRepository : IMedicRepository
{

  private readonly IDbConnection _connection;

  public MedicRepository(IDbConnection connection)
  {
    _connection = connection;
  }

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

      return rows = await _connection.ExecuteAsync(insertSql, new
      {
        schedule.StartsAt,
        schedule.EndsAt,
        schedule.Price,
        schedule.MedicUID
      });
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

      return rows = await _connection.ExecuteAsync(updateSql, new
      {
        schedule.StartsAt,
        schedule.EndsAt,
        schedule.Price,
        schedule.UID
      });
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

      return await _connection.QueryAsync<Schedule>(queySchedule, new
      { guid });
    }
    catch
    {
      return new List<Schedule>();
    }
  }
}
