
using iPractice.Api.UseCases;

namespace iPractice.Api.Models.Psychologists;

public record BookedAppointment : TimeRange
{
    public long ClientId { get; }

    public BookedAppointment(TimeRange slot, long clientId) : base(slot.From, slot.To)
    {
        Id = slot.Id;
        ClientId = clientId;
    }

    private BookedAppointment() : base()
    {

    }

    public CancelledBooking ToCancelledBooking() => new(this);
}