using iPractice.Api.Models.Psychologists;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Controllers.Psychologists.Dtos;

public record PsychologistDetailsDto(long Id, string Name, List<long> AssignedClients,
    List<AvailableTimeSlot> AvailableTimeSlots, List<BookedAppointment> BookedAppointments)
{
    public static PsychologistDetailsDto From(Psychologist psychologist)
    {
        return new(psychologist.Id, psychologist.Name, psychologist.ClientAssignment.ClientIds,
            [.. psychologist.Calendar.AvailableTimeSlots.OrderBy(x => x.From)],
            [.. psychologist.Calendar.BookedAppointments.OrderBy(x => x.From)]);
    }
}