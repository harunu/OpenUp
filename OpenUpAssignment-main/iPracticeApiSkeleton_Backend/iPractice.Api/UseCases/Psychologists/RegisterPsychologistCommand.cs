using iPractice.Api.Models.Psychologists;
using iPractice.Api.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Psychologists;

public class RegisterPsychologistCommand(string name, List<long> initialClients) : IRequest<Psychologist>
{
    public string Name { get; } = name;
    public List<long> InitialClients { get; } = initialClients;
}

public class RegisterPsychologistHandler(IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<RegisterPsychologistCommand, Psychologist>
{
    public async Task<Psychologist> Handle(RegisterPsychologistCommand request, CancellationToken cancellationToken)
    {
        var psychologist = Psychologist.Create(request.Name, request.InitialClients);

        await psychologistSqlRepository.AddPsychologistAsync(psychologist, cancellationToken);

        return psychologist;
    }
}