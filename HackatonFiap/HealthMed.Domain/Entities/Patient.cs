namespace HealthMed.Domain.Entities;
public class Patient
{
  public Patient(string name, string email, string cPF, string password)
  {
    Name = name;
    Email = email;
    CPF = cPF;
    Password = password;
  }

  public Guid UID { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string CPF { get; set; }
  public string Password { get; set; }
}
