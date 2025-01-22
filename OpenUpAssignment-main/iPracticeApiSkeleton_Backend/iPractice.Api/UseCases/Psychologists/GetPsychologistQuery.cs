using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists;

public class GetPsychologistQuery(long psychologistId) : IRequest<Psychologist>
{
    public long PsychologistId { get; } = psychologistId;
}

public class GetPsychologistHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<GetPsychologistQuery, Psychologist>
{
    public async Task<Psychologist> Handle(GetPsychologistQuery request, CancellationToken cancellationToken) =>
        await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);
}