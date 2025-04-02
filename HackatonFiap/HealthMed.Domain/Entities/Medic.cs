using HealthMed.Domain.Enums;

namespace HealthMed.Domain.Entities;
public class Medic
{
  public Medic(){}
  public Medic(string name, string cRM, Specialty specialty, string password)
  {
    Name = name;
    CRM = cRM;
    Specialty = specialty;
    Password = password;
  }

  public Guid UID { get; set; }
  public string Name { get; set; }
  public string CRM { get; set; }
  public Specialty Specialty { get; set; }
  public string Password { get; set; }
}
