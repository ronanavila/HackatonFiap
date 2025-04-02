using HealthMed.Application.Dto;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Extensions;
public static class MedicExtension
{
  public static List<MedicResponseDto> MedicsToMedicsDto(this IEnumerable<Medic> medics)
  {
    var dtos = new List<MedicResponseDto>();
    foreach (var medic in medics)
    {
      dtos.Add(new MedicResponseDto(medic.UID, medic.Name, medic.Specialty));
    }
    return dtos;
  }
}
