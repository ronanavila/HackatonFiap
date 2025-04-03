using HealthMed.Application.Dto;
using TechChallenge.Domain.Contracts;

namespace HealthMed.Application.Contracts;

public interface IPatientService
{
  public Task<IResponse> GetMedicBySpecialty(string scpecialty);
  public Task<IResponse> GetMedicSchedule(Guid uid);
  public Task<IResponse> BookAppointment(ScheduleAppointmentDto appointmentDto, Guid patientUid);
  public Task<IResponse> CancelAppointment(CancelAppointmentDto cancelAppointmentDto, Guid patientUid);
}