using iPractice.Api.Models.Clients;
using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class GetClientQuery(long clientId) : IRequest<Client>
{
    public long ClientId { get; } = clientId;
}

public class GetClientHandler(IClientSqlRepository clientSqlRepository) : IRequestHandler<GetClientQuery, Client>
{
    public async Task<Client> Handle(GetClientQuery request, CancellationToken cancellationToken) =>
        await clientSqlRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
}