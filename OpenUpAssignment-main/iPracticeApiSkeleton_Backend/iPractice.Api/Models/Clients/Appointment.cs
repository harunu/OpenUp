using iPractice.Api.UseCases;

namespace iPractice.Api.Models.Clients;

public record Appointment : TimeRange
{
    public long PsychologistId { get; }

    public Appointment(TimeRange timeRange, long psychologistId) : base(timeRange.From, timeRange.To)
    {
        Id = timeRange.Id;
        PsychologistId = psychologistId;
    }

    private Appointment() : base()
    {

    }

    public CancelledAppointment ToCancelledAppointment() => new(this);
}