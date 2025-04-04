namespace HealthMed.Domain.Entities;
public class Schedule
{
  public Schedule(DateTime startsAt, DateTime endsAt, decimal price, Guid medicGuid)
  {
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicUID = medicGuid;
  }

  public Schedule(Guid uid, DateTime startsAt, DateTime endsAt, decimal price, Guid medicGuid)
  {
    UID = uid;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicUID = medicGuid;
  }

  public Schedule() { }

  public Guid UID { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }
  public Guid MedicUID { get; set; }
  public Guid PatientUID { get; set; }
  public bool Approved { get; set; }

}
