using Flunt.Notifications;
using Flunt.Validations;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Dto;
public class ScheduleCreationDto : Notifiable<Notification>
{
  public ScheduleCreationDto(DateOnly date, TimeOnly startsAt, TimeOnly endsAt, decimal price, string medicCrm)
  {
    Date = date;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
    MedicCrm = medicCrm;
  }

  public DateOnly Date {  get; set; }
  public TimeOnly StartsAt { get; set; }
  public TimeOnly EndsAt { get; set; }
  public decimal Price { get; set; }
  public string MedicCrm { get; set; }

  public Schedule ToSchedule(ScheduleCreationDto scheduleCreationDto)
  {
    return new Schedule(
      scheduleCreationDto.Date,
      scheduleCreationDto.StartsAt,
      scheduleCreationDto.EndsAt,
      scheduleCreationDto.Price
      , scheduleCreationDto.MedicCrm);
  }

  public void Validate()
  {
    AddNotifications(
    new Contract<ScheduleCreationDto>()
    .Requires()
      .IsNotEmpty(Date.ToString(), "Date", "Date não pode ser vazio")
      .IsNotEmpty(StartsAt.ToString(), "StartsAt não pode ser vazio.")
      .IsNotEmpty(EndsAt.ToString(), "EndsAt", "EndsAt não pode ser vazio.")
      .IsGreaterThan(Price, 0, "Price", "Telefone tem que ter no máximo 9 números.")
      .IsNotEmpty(MedicCrm, "MedicCrm", "MedicCrm não pode ser vazio.")
    );
  }
}


