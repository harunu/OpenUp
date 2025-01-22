using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists;

public class AssignNewClientCommand(long psychologistId, long clientId) : IRequest<Psychologist>
{
    public long PsychologistId { get; } = psychologistId;
    public long ClientId { get; } = clientId;
}

public class AssignNewClientHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<AssignNewClientCommand, Psychologist>
{
    public async Task<Psychologist> Handle(AssignNewClientCommand request, CancellationToken cancellationToken)
    {
        var psychologist = await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);

        psychologist.AssignNewClient(request.ClientId);

        await psychologistSqlRepository.SaveChangesAsync(cancellationToken);

        return psychologist;
    }
}