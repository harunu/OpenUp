using iPractice.Api.Models.Psychologists;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.Repositories;

public class PsychologistSqlRepository(ApplicationDbContext dbContext) : IPsychologistSqlRepository
{
    public async Task<Psychologist> GetPsychologistByIdAsync(long id, CancellationToken cancellationToken)
    {
        var psychologist = await dbContext.Psychologists.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return psychologist;
    }

    public async Task<List<Psychologist>> GetPsychologistsByIdsAsync(List<long> ids, CancellationToken cancellationToken)
    {
        var psychologists = await dbContext.Psychologists
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        return psychologists;
    }

    public async Task<Psychologist> AddPsychologistAsync(Psychologist psychologist, CancellationToken cancellationToken)
    {
        await dbContext.Psychologists.AddAsync(psychologist, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return psychologist;
    }

    public async Task DeletePsychologistAsync(Psychologist psychologist, CancellationToken cancellationToken)
    {
        dbContext.Psychologists.Remove(psychologist);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}
