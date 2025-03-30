using HealthMed.Domain.Entities;
using HealthMed.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Repository.Configuration;
internal class MedicConfiguration
{
  //internal void Configure(EntityTypeBuilder<Medic> builder)
  //{
  //  builder.ToTable("Medic");

  //  builder.HasKey(p => p.CRM);

  //  builder.Property(p => p.CRM)
  //      .HasColumnType("UNIQUEIDENTIFIER");

  //  builder.Property(P => P.CRM)
  //      .HasColumnType("VARCHAR(6)")
  //      .IsRequired();

  //  builder.Property(P => P.Name)
  //              .HasColumnType("VARCHAR(150)")
  //              .IsRequired();

  //  builder.Property(p => p.Password)
  //              .HasColumnType("VARCHAR(500)")
  //              .IsRequired();

  //  builder.Property(p => p.Specialty)
  //          .HasColumnType("VARCHAR(100)")
  //          .IsRequired();

  //  builder.HasData(
  //  new Medic ( "Joao", "123456", Specialty.Cardiologist,  "123456"),
  //   new Medic( "Joao2", "123456", Specialty.Cardiologist,  "123456"),
  //   new Medic( "Joao3", "123456", Specialty.Cardiologist,  "123456")
  //);
  //}
}
