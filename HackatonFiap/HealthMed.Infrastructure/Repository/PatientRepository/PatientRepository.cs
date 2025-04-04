using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using System.Data;
using System.Transactions;

namespace HealthMed.Infrastructure.Repository.PatientRepository;
public class PatientRepository : IPatientRepository
{
  private readonly IDbConnection _connection;

  public PatientRepository(IDbConnection connection)
  {
    _connection = connection;
  }

  public async Task<IEnumerable<Medic>> GetMedicsBySpecialty(string specialty)
  {
    try
    {
      var queryMedics = @"SELECT 
        [UID]
        ,[NAME]
        ,[SPECIALTY]
      FROM [MEDIC] WHERE SPECIALTY = @SPECIALTY;";

      return await _connection.QueryAsync<Medic>(queryMedics, new
      { specialty });
    }
    catch
    {
      return new List<Medic>();
    }
  }

  public async Task<IEnumerable<Schedule>> GetMedicBySchedule(Guid uid)
  {
    try
    {
      var queySchedule = @"
        SELECT      
          [UID],
          [STARTSAT],
          [ENDSAT],
          [PRICE],
          [MEDICUID]
      FROM 
        [SCHEDULE] 
      WHERE 
        MEDICUID = @UID
        AND STARTSAT >= GETDATE()
        AND PATIENTUID IS NULL;";

      return await _connection.QueryAsync<Schedule>(queySchedule, new
      { uid });

    }
    catch
    {
      return new List<Schedule>();
    }
  }

  public async Task<int> BookAppointment(Guid uid, Guid patientUid)
  {
    try
    {
      var queySchedule = @"
        UPDATE      
          [SCHEDULE] 
        SET
          [PATIENTUID] = @PATIENTUID
      WHERE 
        UID = @UID
        AND [PATIENTUID] IS NULL;";


      return await _connection.ExecuteAsync(queySchedule, new
      { patientUid, uid });
    }
    catch
    {
      return 0;
    }
  }

  public async Task<int> CancelAppointment(string reason, Guid scheduleUid, Guid patientUid)
  {
    using (var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    {
      try
      {
        var queySchedule = @"
      
        INSERT INTO      
          [CANCELEDSCHEDULES] ([SCHEDULEUID], [PATIENTUID], [REASON])
        VALUES
          (@SCHEDULEUID, @PATIENTUID, @REASON)

        UPDATE
          [SCHEDULE]
        SET
          [PATIENTUID] = NULL,
          [APPROVED] = 0
        WHERE
          [UID] = @SCHEDULEUID";

        int rowsAffected = await _connection.ExecuteAsync(queySchedule, new { scheduleUid, patientUid, reason });
        tran.Complete();
        return rowsAffected;

      }
      catch
      {
        return 0;
      }
      finally
      {
        tran.Dispose();
      }
    }
  }
}

