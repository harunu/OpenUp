using iPractice.Api.Models.Psychologists;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.Repositories;

public interface IPsychologistSqlRepository
{
    Task<Psychologist> GetPsychologistByIdAsync(long id, CancellationToken cancellationToken);
    Task<List<Psychologist>> GetPsychologistsByIdsAsync(List<long> ids, CancellationToken cancellationToken);

    Task<Psychologist> AddPsychologistAsync(Psychologist psychologist, CancellationToken cancellationToken);
    Task DeletePsychologistAsync(Psychologist psychologist, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
