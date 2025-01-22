using iPractice.Api.UseCases;
using System;

namespace iPractice.Api.Models.Clients;

public record BookableTimeSlot : TimeRange
{
    public BookableTimeSlot(DateTime from, DateTime to) : base(from, to)
    {

    }
}