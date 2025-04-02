using HealthMed.Application.Dto;
using TechChallenge.Domain.Contracts;

namespace HealthMed.Application.Contracts;

public interface IPatientService
{
  public Task<IResponse> GetMedicBySpecialty(string scpecialty);
}