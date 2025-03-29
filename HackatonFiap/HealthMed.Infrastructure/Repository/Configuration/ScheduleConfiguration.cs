using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HealthMed.Infrastructure.Repository.Configuration;
internal class ScheduleConfiguration
{
  internal void Configure(EntityTypeBuilder<Schedule> builder)
  {
    builder.ToTable("Schedule");

    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id)
        .HasColumnType("UNIQUEIDENTIFIER")
        .UseIdentityColumn();

    builder.Property(P => P.StartsAt)
        .IsRequired();

    builder.Property(P => P.EndsAt)
        .IsRequired();

    builder.Property(p => p.Price)
    .IsRequired();


  //  builder.HasOne(a => a.MedicCRM)
  //          .WithMany(m => m.CRM)
  //          .HasForeignKey(a => a.CRM)
  //          .OnDelete(DeleteBehavior.Restrict);

  //  builder.Property(p => p.MedicCRM)
  //    .HasForeignKey(a => a.CRM);


  //  builder.Property(p => p.PatientId);

  //  builder.Property(p => p.Approved)
  //     .HasColumnType("BIT")  
  //     .HasDefaultValue(0);


  //  builder.HasOne(p => p.Region)
  //.WithMany(x => x.Contacts)
  //.HasConstraintName("FK_Contact_Region")
  //.OnDelete(DeleteBehavior.NoAction);
  }
}
