using System.Collections.Generic;
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

public class GetAllPsychologistIdsQuery : IRequest<List<long>> { }

public class GetPsychologistHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<GetPsychologistQuery, Psychologist>
{
    public async Task<Psychologist> Handle(GetPsychologistQuery request, CancellationToken cancellationToken) =>
        await psychologistSqlRepository.GetPsychologistByIdAsync(request.PsychologistId, cancellationToken);
}
public class GetAllPsychologistIdsHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<GetAllPsychologistIdsQuery, List<long>>
{
    public async Task<List<long>> Handle(GetAllPsychologistIdsQuery request, CancellationToken cancellationToken) =>
        await psychologistSqlRepository.GetAllPsychologistIdsAsync(cancellationToken);
}