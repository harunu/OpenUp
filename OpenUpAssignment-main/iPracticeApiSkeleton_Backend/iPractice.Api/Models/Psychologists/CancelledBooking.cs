
using iPractice.Api.UseCases;

namespace iPractice.Api.Models.Psychologists;

public record CancelledBooking : TimeRange
{
    public long ClientId { get; }

    public CancelledBooking(BookedAppointment slot) : base(slot.From, slot.To)
    {
        ClientId = slot.ClientId;
        Id = slot.Id;
    }

    public AvailableTimeSlot ToAvailableTimeSlot() => new(this);
}