using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class CancelAppointmentCommand(long clientId, string appointmentId) : IRequest
{
    public long ClientId { get; } = clientId;
    public string AppointmentId { get; } = appointmentId;
}

public class CancelAppointmentHandler(IClientSqlRepository clientSqlRepository, IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<CancelAppointmentCommand>
{
    public async Task Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var client = await clientSqlRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
        var cancelledAppointment = client.CancelBookedAppointment(request.AppointmentId);

        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(cancelledAppointment.PsychologistId, cancellationToken);
        psychologist.CancelBookedAppointment(cancelledAppointment.Id);

        await clientSqlRepository.SaveChangesAsync(cancellationToken);
    }
}
