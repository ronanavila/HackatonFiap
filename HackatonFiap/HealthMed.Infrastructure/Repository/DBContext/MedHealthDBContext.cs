using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HealthMed.Infrastructure.Repository.DBContext;
public class MedHealthDBContext : DbContext
{
  private string _connectionString = string.Empty;
  public MedHealthDBContext()
  {

    IConfiguration configuration =
          new ConfigurationBuilder()
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json")
          .AddEnvironmentVariables()
          .Build();

    _connectionString = configuration.GetConnectionString("IntegrationTestConnection")!;

  }

  public MedHealthDBContext(string connectionString)
  {
    _connectionString = connectionString;
  }

  //public DbSet<Medic> Medic { get; set; }
  public DbSet<Patient> Patient { get; set; }
  public DbSet<Schedule> Schedule { get; set; }


  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      optionsBuilder.UseSqlServer(_connectionString);
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedHealthDBContext).Assembly);
  }
}
