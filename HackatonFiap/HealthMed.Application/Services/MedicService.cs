using Flunt.Notifications;
using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;
using System.Net;
using TechChallenge.Domain.Contracts;
using TechChallenge.Domain.Shared;

namespace HealthMed.Application.Services;
public class MedicService : Notifiable<Notification>, IMedicService
{
  private readonly IMedicRepository _medicRepository;

  public MedicService(IMedicRepository medicRepository)
  {
    _medicRepository = medicRepository;
  }

  public async Task<IResponse> CreateSchedule(ScheduleCreationDto scheduleDto, Guid medicUid)
  {
    scheduleDto.Validate();

    if (!scheduleDto.IsValid)
    {
      return new BaseResponse(HttpStatusCode.BadRequest, false, scheduleDto.Notifications);
    }

    Schedule schedule = scheduleDto;
    schedule.MedicUID = medicUid;

    var schedules = await _medicRepository.GetScheduleByMedicUid(schedule.MedicUID);

    foreach (var sch in schedules)
    {
      if ((sch.StartsAt >= schedule.StartsAt && sch.StartsAt <= schedule.EndsAt)
          || (sch.EndsAt >= schedule.StartsAt && sch.EndsAt <= schedule.EndsAt))
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false,
            new List<Notification>() { new Notification("Agenda", $"Já existe uma agenda existente neste periodo: Id:{sch.UID}, StartsAt{sch.StartsAt} ,EndsAt{sch.EndsAt}") });
      }
    }

    var created = await _medicRepository.CreateSchedule(schedule);

    if (created == 0)
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false,
          new List<Notification>() { new Notification("Agenda", $"Ocorreu um erro ao tentar criar a agenda") });
    }
    return new BaseResponse(HttpStatusCode.OK, true, "Agenda", "Agenda criada com sucesso");
  }

  public async Task<IResponse> EditSchedule(ScheduleUpdateDto scheduleDto, Guid medicUid)
  {
    scheduleDto.Validate();

    if (!scheduleDto.IsValid)
    {
      return new BaseResponse(HttpStatusCode.BadRequest, false, scheduleDto.Notifications);
    }

    Schedule schedule = scheduleDto;
    schedule.MedicUID = medicUid;

    var schedules = await _medicRepository.GetScheduleByMedicUid(schedule.MedicUID);

    foreach (var sch in schedules)
    {
      if ((sch.StartsAt >= schedule.StartsAt && sch.StartsAt <= schedule.EndsAt)
          || (sch.EndsAt >= schedule.StartsAt && sch.EndsAt <= schedule.EndsAt))
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false,
            new List<Notification>() { new Notification("Agenda", $"Já existe uma agenda existente neste periodo: Id:{sch.UID}, StartsAt{sch.StartsAt} ,EndsAt{sch.EndsAt}") });
      }
    }

    var edited = await _medicRepository.EditSchedule(schedule);

    if (edited == 0)
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false,
          new List<Notification>() { new Notification("Agenda", $"Ocorreu um erro ao tentar alterar a agenda") });
    }
    return new BaseResponse(HttpStatusCode.OK, true, "Agenda", "Agenda alterada com sucesso");
  }

  public async Task<IResponse> GetScheduleByMedicUid(Guid medicUid)
  {
    var schedules = await _medicRepository.GetScheduleByMedicUid(medicUid);

    if (schedules.Count() == 0)
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
        new List<Notification>() { new Notification("Agenda", $"Não foi encontrado nenhuma agenda para este Medico") });
    }

    return new BaseResponse(HttpStatusCode.OK, true, "Get Schedule", schedules);
  }
}
