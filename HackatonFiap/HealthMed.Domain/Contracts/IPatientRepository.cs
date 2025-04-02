using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;
public interface IPatientRepository
{
  public Task<IEnumerable<Medic>> GetMedicsBySpecialty(string scpecialty);
}
