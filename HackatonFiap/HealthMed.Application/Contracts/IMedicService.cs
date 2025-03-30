using HealthMed.Application.Dto;
using TechChallenge.Domain.Contracts;

namespace HealthMed.Application.Contracts;
public interface IMedicService
{
  public Task<IResponse> CreateSchedule(ScheduleCreationDto schedule);
  public Task<IResponse> EditSchedule(ScheduleCreationDto schedule);
  public Task<IResponse> GetScheduleByCrm(string crm);
}
