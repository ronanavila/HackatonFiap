using Flunt.Notifications;
using Flunt.Validations;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Dto;
public class ScheduleCreationDto : Notifiable<Notification>
{
  public ScheduleCreationDto(DateTime startsAt, DateTime endsAt, decimal price)
  {
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
  }

  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }

  public Schedule ToSchedule(ScheduleCreationDto scheduleCreationDto, Guid medicUid)
  {
    return new Schedule(
      scheduleCreationDto.StartsAt,
      scheduleCreationDto.EndsAt,
      scheduleCreationDto.Price
      , medicUid);
  }

  public void Validate()
  {
    AddNotifications(
    new Contract<ScheduleCreationDto>()
    .Requires()
      .IsLowerThan(StartsAt, EndsAt, "StarsAt tem que ser menor que o EndsAt")
      .IsNotEmpty(StartsAt.ToString(), "StartsAt não pode ser vazio.")
      .IsNotEmpty(EndsAt.ToString(), "EndsAt", "EndsAt não pode ser vazio.")
      .IsGreaterThan(Price, 0, "Price", "Telefone tem que ter no máximo 9 números.")
    );
  }
}


