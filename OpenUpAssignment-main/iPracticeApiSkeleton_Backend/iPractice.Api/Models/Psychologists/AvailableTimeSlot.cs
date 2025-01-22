using iPractice.Api.UseCases;
using System;

namespace iPractice.Api.Models.Psychologists;

public record AvailableTimeSlot : TimeRange
{
    public AvailableTimeSlot(TimeRange slot) : base(slot.From, slot.To)
    {
        Id = slot.Id;
    }

    public AvailableTimeSlot(DateTime from, DateTime to) : base(from, to)
    {

    }

    private AvailableTimeSlot() : base()
    {

    }
}