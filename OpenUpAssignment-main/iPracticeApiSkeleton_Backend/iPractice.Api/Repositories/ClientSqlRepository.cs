using iPractice.Api.Models.Clients;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.Repositories;

public class ClientSqlRepository(ApplicationDbContext dbContext) : IClientSqlRepository
{
    public async Task<Client> GetClientByIdAsync(long id, CancellationToken cancellationToken)
    {
        var psychologist = await dbContext.Clients.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return psychologist;
    }

    public async Task<Client> AddClientAsync(Client client, CancellationToken cancellationToken)
    {
        await dbContext.Clients.AddAsync(client, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return client;
    }

    public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
    {
        dbContext.Clients.Remove(client);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}