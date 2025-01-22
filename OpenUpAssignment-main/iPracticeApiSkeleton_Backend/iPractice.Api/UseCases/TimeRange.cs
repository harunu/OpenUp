using System;

namespace iPractice.Api.UseCases;

public abstract record TimeRange
{
    public string Id { get; protected set; }
    public DateTime From { get; }
    public DateTime To { get; }

    protected TimeRange() { }

    protected TimeRange(DateTime from, DateTime to)
    {
        Id = Guid.NewGuid().ToString();
        From = from;
        To = to;
    }

    public bool Intersects(TimeRange timeSlot) => From < timeSlot.To && To > timeSlot.From;
}