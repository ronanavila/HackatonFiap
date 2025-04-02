using HealthMed.Application.Dto;
using TechChallenge.Domain.Contracts;

namespace HealthMed.Application.Contracts;
public interface IMedicService
{
  public Task<IResponse> CreateSchedule(ScheduleCreationDto schedule, Guid medicUid);
  public Task<IResponse> EditSchedule(ScheduleUpdateDto schedule, Guid medicUid);
  public Task<IResponse> GetScheduleByMedicUid(Guid medicUid);
}
