using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Transactions;

namespace HealthMed.Infrastructure.Repository.PatientRepository;
public class PatientRepository : IPatientRepository
{
  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";

  public async Task<IEnumerable<Medic>> GetMedicsBySpecialty(string specialty)
  {
    try
    {
      var queryMedics = @"SELECT 
        [UID]
        ,[NAME]
        ,[SPECIALTY]
      FROM [MEDIC] WHERE SPECIALTY = @SPECIALTY;";

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryAsync<Medic>(queryMedics, new
        { specialty });


      }
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

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryAsync<Schedule>(queySchedule, new
        { uid });


      }
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

      using (var connection = new SqlConnection(connString))
      {
        return await connection.ExecuteAsync(queySchedule, new
        { patientUid,uid });


      }
    }
    catch
    {
      return 0;
    }
  }

  public async Task<int> CancelAppointment(string reason, Guid scheduleUid, Guid patientUid)
  {
    using var connection = new SqlConnection(connString);
    await connection.OpenAsync();
    using var transaction = connection.BeginTransaction();
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

      int rowsAffected = await connection.ExecuteAsync(queySchedule, new { scheduleUid , patientUid, reason}, transaction);
      transaction.Commit();
      return rowsAffected;
    }
    catch
    {
      transaction.Rollback();
      return 0;
    }
    finally
    {
      connection.Close();
    }
  }
}
