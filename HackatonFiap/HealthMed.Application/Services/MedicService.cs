using Flunt.Notifications;
using HealthMed.Application.Contracts;
using HealthMed.Application.Dto;
using HealthMed.Domain.Contracts;
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

  public async Task<IResponse> CreateSchedule(ScheduleCreationDto scheduleDto)
  {
    //scheduleDto.Validate();

    //if (!scheduleDto.IsValid)
    //{
    //  return new BaseResponse(HttpStatusCode.BadRequest, false, scheduleDto.Notifications);
    //}

    var schedule = scheduleDto.ToSchedule(scheduleDto);

    var schedules = await _medicRepository.GetScheduleByCrm(schedule.MedicCrm);

    foreach (var sch in schedules) {
      if(sch.Date == schedule.Date &&
        (
          (sch.StartsAt >= schedule.StartsAt && sch.StartsAt <= schedule.EndsAt)
          ||
          (sch.EndsAt >= schedule.StartsAt && sch.EndsAt <= schedule.EndsAt)
        ))
      {
        return new BaseResponse(HttpStatusCode.BadRequest, false, new List<Notification>() { new Notification("Agenda", $"Já existe uma agenda existente neste periodo: Id:{sch.Id}, Date{sch.Date}, StartsAt{sch.StartsAt} ,EndsAt{sch.EndsAt}") });
      }
    }

    var createdSchedule = await _medicRepository.CreateSchedule(schedule);

    return new BaseResponse(HttpStatusCode.OK, true, "Schedule Creation", createdSchedule);
  }

  public Task<IResponse> EditSchedule(ScheduleCreationDto schedule)
  {
    throw new NotImplementedException();
  }

  public Task<IResponse> GetScheduleByCrm(string crm)
  {
    throw new NotImplementedException();
  }

}
