using HealthMed.Domain.Enums;

namespace HealthMed.Application.Dto;
public class MedicResponseDto
{
  public MedicResponseDto(Guid uID, string name, Specialty specialty)
  {
    UID = uID;
    Name = name;
    Specialty = specialty;
  }

  public Guid UID { get; set; }
  public string Name { get; set; }
  public Specialty Specialty { get; set; }

}


