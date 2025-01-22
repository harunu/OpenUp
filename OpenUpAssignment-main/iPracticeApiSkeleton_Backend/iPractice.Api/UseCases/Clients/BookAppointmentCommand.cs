using iPractice.Api.Models.Clients;
using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class BookAppointmentCommand(long clientId, long psychologistId, string availableTimeSlotId) : IRequest<Appointment>
{
    public long ClientId { get; } = clientId;
    public long PsychologistId { get; } = psychologistId;
    public string AvailableTimeSlotId { get; } = availableTimeSlotId;
}

public class BookAppointmentHandler(IClientSqlRepository clientSqlRepository, IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<BookAppointmentCommand, Appointment>
{
    public async Task<Appointment> Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
    {
        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);
        var psychologistAppointment = psychologist.BookAppointment(request.AvailableTimeSlotId, request.ClientId);

        var client = await clientSqlRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
        var clientAppointment = client.BookAppointment(psychologistAppointment, psychologist.Id);

        await clientSqlRepository.SaveChangesAsync(cancellationToken);
        return clientAppointment;
    }
}