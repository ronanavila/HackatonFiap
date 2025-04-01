using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;
public interface IMedicRepository
{
  public Task<IEnumerable<Schedule>> GetScheduleByCrm(string crm);
  public Task<int> CreateSchedule(Schedule schedule);
  public Task<int> EditSchedule(Schedule schedule);
}
