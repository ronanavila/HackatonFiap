namespace HealthMed.Application.Dto;
public class PatientScheduleResponseDto
{
  public PatientScheduleResponseDto(Guid uid, DateTime startsAt, DateTime endsAt, decimal price, Guid medicUid)
  {
    Uid = uid;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicUid = medicUid;
  }

  public Guid Uid { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }
  public Guid MedicUid { get; set; }
}
