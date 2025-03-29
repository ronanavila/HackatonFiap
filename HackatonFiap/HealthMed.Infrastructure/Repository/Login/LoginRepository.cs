using HealthMed.Domain.Contracts;
using HealthMed.Domain.Entities;

namespace HealthMed.Infrastructure.Repository.Login;

public class LoginRepository : ILoginRepository
{

  public async Task<object> Get(string userName, string password, string role)
  {

    var patientLogin = new PatientLogin { EmailCpf = "patient", Password = "Jovem" };
    var medicLogin = new MedicLogin { Crm = "medic", Password = "Jovem" };

    if (role == "patient")
    {
      return patientLogin;
    }

    return medicLogin;
  }
}

