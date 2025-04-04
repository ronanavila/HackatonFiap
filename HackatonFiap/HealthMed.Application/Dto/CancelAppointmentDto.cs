using Flunt.Notifications;
using Flunt.Validations;

namespace HealthMed.Application.Dto;
public class CancelAppointmentDto : Notifiable<Notification>
{
  public Guid ScheduleUid { get; set; }
  public string Reason { get; set; } = string.Empty;

  public void Validate()
  {
    AddNotifications(
    new Contract<CancelAppointmentDto>()
    .Requires()
      .IsNotEmpty(ScheduleUid, "ScheduleUid não pode ser vazio")
      .IsNotEmpty(Reason, "Reason", "Reason não pode ser vazio.")
    );
  }
}
