using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Repository.Medic;
public class MedicRepository : IMedicRepository
{
  public async Task<Schedule> CreateSchedule(Schedule schedule)
  {
    return schedule;
  }

  public Task<Schedule> EditSchedule(Schedule schedule)
  {
    throw new NotImplementedException();
  }

  public async Task<List<Schedule>> GetScheduleByCrm(string crm)
  {
    return  new List<Schedule> { new Schedule (DateOnly.Parse("10/01/2025"), TimeOnly.Parse("00:00:00"), TimeOnly.Parse("00:10:00"), 10.00m, "500") };
  }
}
