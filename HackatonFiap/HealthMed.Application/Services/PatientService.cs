using Flunt.Notifications;
using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using HealthMed.Domain.Contracts;
using System.Net;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;

namespace HealthMed.Application.Services;
public class PatientService : IPatientService
{

  private readonly IPatientRepository _patientRepository;

  public PatientService(IPatientRepository patientRepository)
  {
    _patientRepository = patientRepository;
  }
  public async Task<IResponse> GetMedicBySpecialty(string scpecialty)
  {
  

    var medics = await _patientRepository.GetMedicsBySpecialty(scpecialty);

    if (medics.Count() == 0)
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
          new List<Notification>() { new Notification("Médico ", $"Nenhum Médico encontrado") });
    }

    List<MedicResponseDto> dtos = [];
    foreach (var medic in medics)
    {
      dtos.Add(new MedicResponseDto(medic.UID, medic.Name, medic.Specialty));
    }
    return new BaseResponse(HttpStatusCode.OK, true, "Médico ", dtos);
  }
}
