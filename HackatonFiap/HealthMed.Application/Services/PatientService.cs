using Flunt.Notifications;
using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using HealthMed.Application.Extensions;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
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

    var dtos = medics.MedicsToMedicsDto();

    return new BaseResponse(HttpStatusCode.OK, true, "Médico ", dtos);
  }


  public async Task<IResponse> GetMedicSchedule(Guid uid)
  {
    IEnumerable<Schedule>? schedules = await _patientRepository.GetMedicBySchedule(uid);

    if (schedules.Count() == 0)
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
          new List<Notification>() { new Notification("Agenda ", $"Nenhuma agenda disponivel") });
    }

    var dtos = schedules.SchedulesToPatientSchedulesDto();

    return new BaseResponse(HttpStatusCode.OK, true, "Agenda ", dtos);
  }
  public async Task<IResponse> BookAppointment(ScheduleAppointmentDto appointmentDto, Guid patientUid)
  {
    var scheduled = await _patientRepository.BookAppointment(appointmentDto.Uid, patientUid);

    if (scheduled == 0)
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
          new List<Notification>() { new Notification("Agenda ", $"Não foi possivel agendar a consulta") });
    }

    return new BaseResponse(HttpStatusCode.OK, true, "Agenda ", "Consulta agendada com sucesso.");
  }

  public async Task<IResponse> CancelAppointment(CancelAppointmentDto cancelAppointmentDto, Guid patientUid)
  {
    cancelAppointmentDto.Validate();
    if (!cancelAppointmentDto.IsValid)
    {
      return new BaseResponse(HttpStatusCode.BadRequest, false, cancelAppointmentDto.Notifications);
    }

    var canceledScheduled = await _patientRepository.CancelAppointment(cancelAppointmentDto.Reason, cancelAppointmentDto.ScheduleUid, patientUid);

    if (canceledScheduled == 0)
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
          new List<Notification>() { new Notification("Agenda ", $"Não foi possivel cancelar o agendamento") });
    }

    return new BaseResponse(HttpStatusCode.OK, true, "Agenda ", "Agendamento cancelado com sucesso.");
  }
}
