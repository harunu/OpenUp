using iPractice.Api.Models.Clients;
using iPractice.Api.Models.Psychologists;
using Microsoft.EntityFrameworkCore;

namespace iPractice.Api;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Psychologist> Psychologists { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Psychologist>(psychologist =>
        {
            psychologist.HasKey(p => p.Id);
            psychologist.OwnsOne(p => p.ClientAssignment, clientAssignment =>
            {
                clientAssignment
                    .Property(c => c.ClientIds)
                    .HasColumnName("AssignedClientIds");
            });

            psychologist.OwnsOne(p => p.Calendar, calendar =>
            {
                calendar.OwnsMany(c => c.AvailableTimeSlots, availableTimeSlot =>
                {
                    availableTimeSlot.HasKey(ats => ats.Id);
                    availableTimeSlot.Property<long>("PsychologistId");
                    availableTimeSlot.WithOwner().HasForeignKey("PsychologistId");
                    availableTimeSlot.Property(ats => ats.From); // no public setters
                    availableTimeSlot.Property(ats => ats.To); // no public setters
                    availableTimeSlot.ToTable("AvailableTimeSlotsOfPsychologists");
                });

                calendar.OwnsMany(c => c.BookedAppointments, bookedAppointment =>
                {
                    bookedAppointment.HasKey(ba => ba.Id);
                    bookedAppointment.Property<long>("PsychologistId");
                    bookedAppointment.WithOwner().HasForeignKey("PsychologistId");
                    bookedAppointment.Property(ba => ba.ClientId); // no public setters
                    bookedAppointment.Property(ba => ba.From); // no public setters
                    bookedAppointment.Property(ba => ba.To); // no public setters
                    bookedAppointment.ToTable("BookedAppointmentsOfPsychologists");
                });
            });
        });

        modelBuilder.Entity<Client>(client =>
        {
            client.HasKey(p => p.Id);
            client.OwnsOne(p => p.PsychologistAssignment, clientAssignment =>
            {
                clientAssignment
                    .PrimitiveCollection(c => c.PsychologistIds)
                    .HasColumnName("AssignedPsychologistIds");
            });

            client.OwnsOne(p => p.Calendar, calendar =>
            {
                calendar.OwnsMany(c => c.Appointments, bookedAppointment =>
                {
                    bookedAppointment.HasKey(ba => ba.Id);
                    bookedAppointment.Property<long>("ClientId");
                    bookedAppointment.WithOwner().HasForeignKey("ClientId");
                    bookedAppointment.Property(ba => ba.PsychologistId); // no public setters
                    bookedAppointment.Property(ba => ba.From); // no public setters
                    bookedAppointment.Property(ba => ba.To); // no public setters
                    bookedAppointment.ToTable("ClientScheduledAppointments");
                });
            });
        });
    }
}
