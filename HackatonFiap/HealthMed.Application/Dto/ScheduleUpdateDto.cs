using Flunt.Notifications;
using Flunt.Validations;
using HealthMed.Domain.Entities;

namespace HealthMed.Application.Dto;
public class ScheduleUpdateDto : Notifiable<Notification>
{
  public ScheduleUpdateDto(int id, DateTime startsAt, DateTime endsAt, decimal price)
  {
    Id = id;
    StartsAt = startsAt;
    EndsAt = endsAt;
    Price = price;
  }

  public int Id { get; set; }
  public DateTime StartsAt { get; set; }
  public DateTime EndsAt { get; set; }
  public decimal Price { get; set; }

  public Schedule ToSchedule(ScheduleUpdateDto scheduleUpdateDto, string crm)
  {
    return new Schedule(
        scheduleUpdateDto.Id,
      scheduleUpdateDto.StartsAt,
      scheduleUpdateDto.EndsAt,
      scheduleUpdateDto.Price
      , crm);
  }

  public void Validate()
  {
    AddNotifications(
    new Contract<ScheduleCreationDto>()
    .Requires()
      .IsGreaterThan(Id,0,"Id nao pode ser 0")
      .IsNotEmpty(StartsAt.ToString(), "StartsAt não pode ser vazio.")
      .IsNotEmpty(EndsAt.ToString(), "EndsAt", "EndsAt não pode ser vazio.")
      .IsGreaterThan(Price, 0, "Price", "Telefone tem que ter no máximo 9 números.")
    );
  }
}


