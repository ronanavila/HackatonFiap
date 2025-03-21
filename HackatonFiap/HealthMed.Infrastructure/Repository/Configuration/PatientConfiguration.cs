using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Infrastructure.Repository.Configuration;
internal class PatientConfiguration
{
  internal void Configure(EntityTypeBuilder<Patient> builder)
  {
    builder.ToTable("Patient");

    builder.HasKey(p => p.CPF);

    builder.Property(p => p.CPF)
        .HasColumnType("UNIQUEIDENTIFIER");

    builder.Property(P => P.CPF)
        .HasColumnType("VARCHAR(11)")
        .IsRequired();

    builder.Property(P => P.Name)
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

    builder.Property(p => p.Password)
                .HasColumnType("VARCHAR(500)")
                .IsRequired();

    builder.Property(p => p.Email)
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

    builder.HasData(
    new Patient("Joao", "joao@email.com", "12345678910", "123456"),
     new Patient("Joao2", "joa2o@email.com", "12345678912", "123456"),
     new Patient("Joao3", "joao3@email.com", "12345678911", "123456")
  );
  }
}
