using HealthMed.Application.Dto;
using TechChallenge.Domain.Contracts;

namespace HealthMed.Application.Contracts;
public interface IMedicService
{
  public Task<IResponse> CreateSchedule(ScheduleCreationDto schedule, string crm);
  public Task<IResponse> EditSchedule(ScheduleUpdateDto schedule, string crm);
  public Task<IResponse> GetScheduleByCrm(string crm);
}
