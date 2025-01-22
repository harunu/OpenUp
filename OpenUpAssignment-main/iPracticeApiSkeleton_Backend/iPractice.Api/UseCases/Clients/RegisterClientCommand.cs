using iPractice.Api.Models.Clients;
using iPractice.Api.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class RegisterClientCommand(string name, List<long> initialPsychologists) : IRequest<Client>
{
    public string Name { get; } = name;
    public List<long> InitialPsychologists { get; } = initialPsychologists;
}

public class RegisterClientHandler(IClientSqlRepository clientSqlRepository) : IRequestHandler<RegisterClientCommand, Client>
{
    public async Task<Client> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var client = Client.Create(request.Name, request.InitialPsychologists);

        await clientSqlRepository.AddClientAsync(client, cancellationToken);

        return client;
    }
}