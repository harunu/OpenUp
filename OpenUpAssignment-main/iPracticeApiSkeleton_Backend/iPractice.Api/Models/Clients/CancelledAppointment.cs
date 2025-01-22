using iPractice.Api.UseCases;

namespace iPractice.Api.Models.Clients;

public record CancelledAppointment : TimeRange
{
    public long PsychologistId { get; }

    public CancelledAppointment(Appointment appointment) : base(appointment.From, appointment.To)
    {
        PsychologistId = appointment.PsychologistId;
        Id = appointment.Id;
    }
}