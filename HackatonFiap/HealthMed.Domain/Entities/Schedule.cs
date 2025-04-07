namespace HealthMed.Domain.Entities;
public class Schedule
{
  public Schedule(DateTime startsAt, DateTime endsAt, decimal price)
  {
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
  }

  public Schedule(Guid uid, DateTime startsAt, DateTime endsAt, decimal price)
  {
    UID = uid;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
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
