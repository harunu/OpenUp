using iPractice.Api.Models.Clients;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.Repositories;

public interface IClientSqlRepository
{
    Task<Client> GetClientByIdAsync(long id, CancellationToken cancellationToken);
    Task<Client> AddClientAsync(Client psychologist, CancellationToken cancellationToken);
    Task DeleteClientAsync(Client psychologist, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}