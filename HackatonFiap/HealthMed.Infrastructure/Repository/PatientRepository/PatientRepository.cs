using Dapper;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;

namespace HealthMed.Infrastructure.Repository.PatientRepository;
public class PatientRepository : IPatientRepository
{
  private string connString = "Server=localhost,1433;Database=HEALTHMED;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False;TrustServerCertificate=True;";
  public async Task<IEnumerable<Medic>> GetMedicsBySpecialty(string specialty)
  {
    try
    {
      var queySchedule = @"SELECT 
        [UID]
        ,[NAME]
        ,[SPECIALTY]
      FROM [MEDIC] WHERE SPECIALTY = @SPECIALTY;";

      using (var connection = new SqlConnection(connString))
      {
        return await connection.QueryAsync<Medic>(queySchedule, new
        { specialty });


      }
    }
    catch
    {
      return new List<Medic>();
    }
  }
}
