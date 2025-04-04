namespace HealthMed.Domain.Entities;

public static class Settings
{
  public static string Secret { get; set; } = string.Empty;
  public static string ConnectionString { get; set; } = string.Empty;
}