using iPractice.Api.Controllers.Psychologists.Dtos;
using iPractice.Api.UseCases.Psychologists;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPractice.Api.Controllers.Psychologists;

[ApiController]
[Route("[controller]")]
public class PsychologistController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new psychologist.
    /// </summary>
    /// <param name="data">The psychologist's name and the IDs of the initially assigned clients.</param>
    /// <returns>The profile of the newly created psychologist</returns>
    [HttpPost]
    public async Task<ActionResult<PsychologistDetailsDto>> RegisterPsychologist([FromBody] CreatePsychologistDto data)
    {
        var psychologist = await mediator.Send(new RegisterPsychologistCommand(data.Name, data.InitialClients));
        return Created($"/psychologists/{psychologist.Id}", PsychologistDetailsDto.From(psychologist));
    }

    /// <summary>
    /// Gets the profile of a psychologist.
    /// </summary>
    /// <param name="id">The psychologist's ID.</param>
    /// <returns>The profile of the psychologist</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PsychologistDetailsDto>> GetPsychologist([FromRoute] long id)
    {
        var psychologist = await mediator.Send(new GetPsychologistQuery(id));
        return Ok(PsychologistDetailsDto.From(psychologist));
    }

    /// <summary>
    /// Assigns a new client to a psychologist.
    /// </summary>
    /// <param name="id">The psychologist's ID.</param>
    /// <param name="client">The client's ID.</param>
    /// <returns>The profile of the psychologist</returns>
    [HttpPost("{id}/clients")]
    public async Task<ActionResult<PsychologistDetailsDto>> AssignNewClient([FromRoute] long id, [FromBody] ClientDto client)
    {
        var psychologist = await mediator.Send(new AssignNewClientCommand(id, client.ClientId));
        return Ok(PsychologistDetailsDto.From(psychologist));
    }

    /// <summary>
    /// Creates a new available timeslot for a psychologist
    /// </summary>
    /// <param name="id">The psychologist's ID.</param>
    /// <param name="timeSlot">The timeslot to mark as available.</param>
    /// <returns>The profile of the psychologist</returns>
    [HttpPost("{id}/available-timeslots")]
    public async Task<ActionResult<PsychologistDetailsDto>> CreateNewAvailableTimeSlot([FromRoute] long id, [FromBody] TimeSlotDto timeSlot)
    {
        var psychologist = await mediator.Send(new CreateNewAvailableTimeSlotCommand(id, timeSlot.From, timeSlot.To));
        return Ok(PsychologistDetailsDto.From(psychologist));
    }

    /// <summary>
    /// Updates an existing available timeslot for a psychologist
    /// </summary>
    /// <param name="id">The psychologist's ID.</param>
    /// <param name="timeSlotId">The timeslot ID to update.</param>
    /// <param name="updatedTimes">The updated times.</param>
    /// <returns>The profile of the psychologist</returns>    
    [HttpPatch("{id}/available-timeslots/{timeSlotId}")]
    public async Task<ActionResult<PsychologistDetailsDto>> UpdateNewAvailableTimeSlot([FromRoute] long id, [FromRoute] string timeSlotId,
        [FromBody] TimeSlotDto updatedTimes)
    {
        var psychologist = await mediator.Send(new UpdateAvailableTimeSlotCommand(id, timeSlotId, updatedTimes.From, updatedTimes.To));
        return Ok(PsychologistDetailsDto.From(psychologist));
    }
}
