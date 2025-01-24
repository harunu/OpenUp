using iPractice.Api.UseCases;
using System;
namespace iPractice.Api.Models.Psychologists;

public record AvailableTimeSlot : TimeRange
{
    public long? PsychologistId { get; init; } 
    
    public AvailableTimeSlot(TimeRange slot, long? psychologistId = null) : base(slot.From, slot.To)
    {
        Id = slot.Id ?? Guid.NewGuid().ToString();
        PsychologistId = psychologistId; 
    }
    public AvailableTimeSlot(DateTime from, DateTime to, long? psychologistId = null) : base(from, to)
    {
        Id = Guid.NewGuid().ToString();
        PsychologistId = psychologistId;
    }
    private AvailableTimeSlot() : base() { }
}