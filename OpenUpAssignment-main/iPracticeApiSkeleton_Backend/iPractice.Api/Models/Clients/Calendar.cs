using iPractice.Api.UseCases;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models.Clients;

public class Calendar
{
    public List<Appointment> Appointments { get; } = [];

    private Calendar()
    {

    }

    public static Calendar Empty() => new();

    public Appointment BookAppointment(TimeRange timeSlot, long psychologistId)
    {
        var newAppointment = new Appointment(timeSlot, psychologistId);
        Appointments.Add(newAppointment);

        return newAppointment;
    }

    public CancelledAppointment CancelAppointment(string appointmentId)
    {
        var appointment = Appointments.SingleOrDefault(x => x.Id == appointmentId);

        Appointments.Remove(appointment);

        return appointment.ToCancelledAppointment();
    }
}