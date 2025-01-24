using iPractice.Api.Models.Clients;
using iPractice.Api.Models.Psychologists;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Api;

public class ApplicationDbContext : DbContext
{
    public DbSet<Psychologist> Psychologists { get; set; }
    public DbSet<Client> Clients { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurePsychologistEntity(modelBuilder);
        ConfigureClientEntity(modelBuilder);
    }

    private static void ConfigurePsychologistEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Psychologist>(psychologist =>
        {
            psychologist.HasKey(p => p.Id);

            // ClientAssignment mapping
            psychologist.OwnsOne(p => p.ClientAssignment, clientAssignment =>
            {
                clientAssignment.Property(c => c.ClientIds).HasColumnName("AssignedClientIds");
            });

            // Calendar mapping
            psychologist.OwnsOne(p => p.Calendar, calendar =>
            {
                calendar.Property<bool>("IsInitialized")
                    .HasDefaultValue(true)
                    .IsRequired();

                calendar.OwnsMany(c => c.AvailableTimeSlots, availableTimeSlot =>
                {
                    availableTimeSlot.HasKey(ats => ats.Id);
                    availableTimeSlot.Property(ats => ats.Id).IsRequired();
                    availableTimeSlot.Property(ats => ats.From).IsRequired();
                    availableTimeSlot.Property(ats => ats.To).IsRequired();
                    availableTimeSlot.WithOwner().HasForeignKey("PsychologistId");
                    availableTimeSlot.ToTable("AvailableTimeSlotsOfPsychologists");
                });
                
                calendar.OwnsMany(c => c.BookedAppointments, bookedAppointment =>
                {
                    bookedAppointment.HasKey(ba => ba.Id);
                    bookedAppointment.WithOwner().HasForeignKey("PsychologistId");
                    bookedAppointment.Property(ba => ba.ClientId).IsRequired();
                    bookedAppointment.Property(ba => ba.From).IsRequired();
                    bookedAppointment.Property(ba => ba.To).IsRequired();
                    bookedAppointment.ToTable("BookedAppointmentsOfPsychologists");
                });
            });
        });
    }

    private static void ConfigureClientEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(client =>
        {
            client.HasKey(p => p.Id);

            // PsychologistAssignment mapping
            client.OwnsOne(p => p.PsychologistAssignment, psychologistAssignment =>
            {
                psychologistAssignment.Property(pa => pa.PsychologistIds).HasColumnName("AssignedPsychologistIds");
            });

            // Calendar mapping
            client.OwnsOne(p => p.Calendar, calendar =>
            {
                calendar.Property<bool>("IsInitialized")
                    .HasDefaultValue(true)
                    .IsRequired();

                calendar.OwnsMany(c => c.Appointments, bookedAppointment =>
                {
                    bookedAppointment.HasKey(ba => ba.Id);
                    bookedAppointment.WithOwner().HasForeignKey("ClientId");
                    bookedAppointment.Property(ba => ba.PsychologistId).IsRequired();
                    bookedAppointment.Property(ba => ba.From).IsRequired();
                    bookedAppointment.Property(ba => ba.To).IsRequired();
                    bookedAppointment.ToTable("ClientScheduledAppointments");
                });
            });
        });
    }
}