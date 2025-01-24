using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class CancelAppointmentCommand(long clientId, string appointmentId) : IRequest<bool>
{
    public long ClientId { get; } = clientId;
    public string AppointmentId { get; } = appointmentId;
}

public class CancelAppointmentHandler(IClientSqlRepository clientSqlRepository, IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<CancelAppointmentCommand, bool>
{
    public async Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var client = await clientSqlRepository.GetClientByIdAsync(request.ClientId, cancellationToken);

        if (client == null)
        {
            return false;
        }
        var cancelledAppointment = client.CancelBookedAppointment(request.AppointmentId);

        if (cancelledAppointment == null)
        {
            return false;
        }
        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(cancelledAppointment.PsychologistId, cancellationToken);

        if (psychologist == null)
        {
            return false;
        }

        psychologist.CancelBookedAppointment(cancelledAppointment.Id);

        await clientSqlRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
