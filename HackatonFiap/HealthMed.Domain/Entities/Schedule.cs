namespace HealthMed.Domain.Entities;
public class Schedule
{
  public Schedule(DateOnly date, TimeOnly startsAt, TimeOnly endsAt, decimal price, string medicCrm)
  {
    Date = date;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicCrm = medicCrm;
  }

  public int Id { get; set; }

  public DateOnly Date { get; set; }
  public TimeOnly StartsAt { get; set; }
  public TimeOnly EndsAt { get; set; }
  public decimal Price { get; set; }
  public string MedicCrm { get; set; } 
  public string PatientCPF { get; set; } = string.Empty;
  public bool Approved { get; set; }

  public virtual Medic? Medic { get; set; }
  public virtual Patient? Patient { get; set; }
}
