using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;
public interface IMedicRepository
{
  public Task<IEnumerable<Schedule>> GetScheduleByMedicUid(Guid medicUid);
  public Task<int> CreateSchedule(Schedule schedule);
  public Task<int> EditSchedule(Schedule schedule);
  public Task<int> ConfirmMedicUid(Guid scheduleUid, bool confirmed, Guid medicUid);
}
