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

  public async Task<IResponse> CreateSchedule(ScheduleCreationDto scheduleDto, string crm)
  {
    scheduleDto.Validate();

    if (!scheduleDto.IsValid)
    {
      return new BaseResponse(HttpStatusCode.BadRequest, false, scheduleDto.Notifications);
    }

    var schedule = scheduleDto.ToSchedule(scheduleDto, crm);

    var schedules = await _medicRepository.GetScheduleByCrm(schedule.MedicCrm);

    foreach (var sch in schedules) {
      if((sch.StartsAt >= schedule.StartsAt && sch.StartsAt <= schedule.EndsAt)
          || (sch.EndsAt >= schedule.StartsAt && sch.EndsAt <= schedule.EndsAt))
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, 
            new List<Notification>() { new Notification("Agenda", $"Já existe uma agenda existente neste periodo: Id:{sch.Id}, StartsAt{sch.StartsAt} ,EndsAt{sch.EndsAt}") });
      }
    }

    var created = await _medicRepository.CreateSchedule(schedule);

    if(created == 0)
    {
      return new BaseResponse(HttpStatusCode.InternalServerError, false, 
          new List<Notification>() { new Notification("Agenda", $"Ocorreu um erro ao tentar criar a agenda") });
    }
    return new BaseResponse(HttpStatusCode.OK, true, "Agenda","Agenda criada com sucesso" );
  }

  public async Task<IResponse> EditSchedule(ScheduleUpdateDto scheduleDto, string crm)
  {
    scheduleDto.Validate();

    if (!scheduleDto.IsValid)
    {
      return new BaseResponse(HttpStatusCode.BadRequest, false, scheduleDto.Notifications);
    }

    var schedule = scheduleDto.ToSchedule(scheduleDto, crm);

    var schedules = await _medicRepository.GetScheduleByCrm(schedule.MedicCrm);

    foreach (var sch in schedules)
    {
      if ((sch.StartsAt >= schedule.StartsAt && sch.StartsAt <= schedule.EndsAt)
          || (sch.EndsAt >= schedule.StartsAt && sch.EndsAt <= schedule.EndsAt))
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false,
            new List<Notification>() { new Notification("Agenda", $"Já existe uma agenda existente neste periodo: Id:{sch.Id}, StartsAt{sch.StartsAt} ,EndsAt{sch.EndsAt}") });
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

  public async Task<IResponse> GetScheduleByCrm(string crm)
  {
    var schedules = await _medicRepository.GetScheduleByCrm(crm);

    if(schedules.Count() == 0 )
    {
      return new BaseResponse(HttpStatusCode.NotFound, false,
        new List<Notification>() { new Notification("Agenda", $"Não foi encontrado nenhuma agenda para este CRM") });
    }

    return new BaseResponse(HttpStatusCode.OK, true, "Get Schedule", schedules);
  }
}
