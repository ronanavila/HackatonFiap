namespace HealthMed.Domain.Entities;
public class Schedule
{
  public Schedule(int id,TimeOnly startsAt, TimeOnly endsAt, decimal price, string medicCrm)
  {
    Id = id;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicCRM = medicCrm;
  }

  public int Id { get; set; }
  public TimeOnly StartsAt { get; set; }
  public TimeOnly EndsAt { get; set; }
  public decimal Price { get; set; }
  public string MedicCRM { get; set; }
  public string PatientCPF { get; set; }
  public bool Approved { get; set; }

  public virtual Medic? Medic { get; set; }
  public virtual Patient? Patient { get; set; }
}
