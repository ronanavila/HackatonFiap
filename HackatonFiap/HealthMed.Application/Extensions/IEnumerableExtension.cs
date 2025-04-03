using HealthMed.Application.Dto;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Extensions;
public static class IEnumerableExtension
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

  public static List<PatientScheduleResponseDto> SchedulesToPatientSchedulesDto(this IEnumerable<Schedule> schedules)
  {
    var dtos = new List<PatientScheduleResponseDto>();
    foreach (var schedule in schedules)
    {
      dtos.Add(new PatientScheduleResponseDto(schedule.UID, schedule.StartsAt, schedule.EndsAt, schedule.Price, schedule.MedicUID));
    }
    return dtos;
  }
}
