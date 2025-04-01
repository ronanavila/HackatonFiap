namespace HealthMed.Domain.Entities;
public class Schedule
{
  public Schedule(DateTime startsAt, DateTime endsAt, decimal price, string medicCrm)
  {
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicCrm = medicCrm;
  }

  public Schedule(int id, DateTime startsAt, DateTime endsAt, decimal price, string medicCrm)
  {
    Id = id;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicCrm = medicCrm;
  }

  public Schedule(){}

  public int Id { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }
  public string MedicCrm { get; set; } 
  public string PatientCPF { get; set; } = string.Empty;
  public bool Approved { get; set; }

}
