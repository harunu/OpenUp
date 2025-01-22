using iPractice.Api.Controllers.Clients.Dtos;
using iPractice.Api.Models.Clients;
using iPractice.Api.UseCases.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPractice.Api.Controllers.Clients;

[ApiController]
[Route("[controller]")]
public class ClientController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <param name="data">The client's name and the IDs of the initially assigned psychologists.</param>
    /// <returns>The profile of the newly created client</returns>
    [HttpPost]
    public async Task<ActionResult<ClientDetailsDto>> RegisterClient([FromBody] CreateClientDto data)
    {
        var client = await mediator.Send(new RegisterClientCommand(data.Name, data.InitialPsychologistIds));
        return Created($"/clients/{client.Id}", ClientDetailsDto.From(client));
    }

    /// <summary>
    /// Gets the profile of a client.
    /// </summary>
    /// <param name="id">The client's ID.</param>
    /// <returns>The profile of the client</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDetailsDto>> GetClient([FromRoute] long id)
    {
        var client = await mediator.Send(new GetClientQuery(id));
        return Ok(ClientDetailsDto.From(client));
    }

    /// <summary>
    /// The client can see when his psychologists are available.
    /// Get available slots from his two psychologists.
    /// </summary>
    /// <param name="id">The client ID</param>
    /// <returns>All time slots for the selected client</returns>
    [HttpGet("{id}/available-timeslots")]
    public async Task<ActionResult<List<AvailableTimeSlotsResult>>> GetAvailableTimeSlots([FromRoute] long id)
    {
        var availableTimeSlots = await mediator.Send(new GetAvailableTimeSlotsQuery(id));
        return Ok(availableTimeSlots);
    }

    /// <summary>
    /// Create an appointment for a given availability slot
    /// </summary>
    /// <param name="id">The client ID</param>
    /// <param name="data">Identifies the psychologist and availability slot</param>
    /// <returns>The newly created booking</returns>
    [HttpPost("{id}/bookings")]
    public async Task<ActionResult<Appointment>> BookAppointment([FromRoute] long id, [FromBody] CreateBookingDto data)
    {
        var appointment = await mediator.Send(new BookAppointmentCommand(id, data.PsychologistId, data.AvailableTimeSlotId));
        return Ok(appointment);
    }

    /// <summary>
    /// Cancels an appointment
    /// </summary>
    /// <param name="id">The client ID</param>
    /// <param name="appointmentId">The ID of the appointmentId to be cancelled</param>
    /// <returns>No Content, if appointment was cancelled</returns>
    [HttpDelete("{id}/bookings/{appointmentId}")]
    public async Task<ActionResult> CancelAppointment([FromRoute] long id, [FromRoute] string appointmentId)
    {
        await mediator.Send(new CancelAppointmentCommand(id, appointmentId));
        return NoContent();
    }
}
