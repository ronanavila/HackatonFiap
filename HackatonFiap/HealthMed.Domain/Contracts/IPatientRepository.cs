using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;
public interface IPatientRepository
{
  public Task<IEnumerable<Medic>> GetMedicsBySpecialty(string scpecialty);
  public Task<IEnumerable<Schedule>> GetMedicBySchedule(Guid uid);
  public Task<int> BookAppointment(Guid uid, Guid patientUid);
  public Task<int> CancelAppointment(string reason, Guid scheduleUid, Guid patientUid);
}
