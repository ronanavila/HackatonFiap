using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Contracts;
public interface IMedicRepository
{
  public Task<List<Schedule>> GetScheduleByCrm(string crm);
  public Task<Schedule> CreateSchedule(Schedule schedule);
  public Task<Schedule> EditSchedule(Schedule schedule);
}
