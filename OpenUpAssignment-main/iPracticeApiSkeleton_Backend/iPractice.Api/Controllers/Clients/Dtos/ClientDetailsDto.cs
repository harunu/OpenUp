using iPractice.Api.Models.Clients;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Controllers.Clients.Dtos;

public record ClientDetailsDto(long Id, string Name, List<long> AssignedPsychologists,
    List<Appointment> BookedAppointments)
{
    public static ClientDetailsDto From(Client client)
    {
        return new(client.Id, client.Name, client.PsychologistAssignment.PsychologistIds,
            [.. client.Calendar.Appointments.OrderBy(x => x.From)]);
    }
}
