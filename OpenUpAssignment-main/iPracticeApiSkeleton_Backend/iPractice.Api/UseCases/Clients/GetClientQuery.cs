using iPractice.Api.Models.Clients;
using iPractice.Api.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class GetClientQuery : IRequest<Client>
{
    public long ClientId { get; }
    public GetClientQuery(long clientId) => ClientId = clientId;
}

public class GetClientHandler : IRequestHandler<GetClientQuery, Client>
{
    private readonly IClientSqlRepository _repository;

    public GetClientHandler(IClientSqlRepository repository)
    {
        _repository = repository;
    }

    public async Task<Client> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetClientByIdAsync(request.ClientId, cancellationToken);
    }
}