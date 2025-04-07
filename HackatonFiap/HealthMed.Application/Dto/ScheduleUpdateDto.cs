using Flunt.Notifications;
using Flunt.Validations;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Dto;
public class ScheduleUpdateDto : Notifiable<Notification>
{
  public ScheduleUpdateDto(Guid uid, DateTime startsAt, DateTime endsAt, decimal price)
  {
    UID = uid;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
  }

  public Guid UID { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }

  public static implicit operator Schedule(ScheduleUpdateDto scheduleUpdateDto)
  {
    return new Schedule(
        scheduleUpdateDto.UID,
      scheduleUpdateDto.StartsAt,
      scheduleUpdateDto.EndsAt,
      scheduleUpdateDto.Price);
  }

  public void Validate()
  {
    AddNotifications(
    new Contract<ScheduleCreationDto>()
    .Requires()
      .IsNotEmpty(UID, "UID nao pode ser vazio")
      .IsLowerThan(StartsAt, EndsAt, "StarsAt tem que ser menor que o EndsAt")
      .IsNotEmpty(StartsAt.ToString(), "StartsAt não pode ser vazio.")
      .IsNotEmpty(EndsAt.ToString(), "EndsAt", "EndsAt não pode ser vazio.")
      .IsGreaterThan(Price, 0, "Price", "Telefone tem que ter no máximo 9 números.")
    );
  }
}


