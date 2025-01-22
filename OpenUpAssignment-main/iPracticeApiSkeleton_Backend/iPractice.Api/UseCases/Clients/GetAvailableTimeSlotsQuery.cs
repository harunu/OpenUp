using iPractice.Api.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iPractice.Api.UseCases.Clients;

public class GetAvailableTimeSlotsQuery(long clientId) : IRequest<List<AvailableTimeSlotsResult>>
{
    public long ClientId { get; } = clientId;
}

public class GetAvailableTimeSlotsHandler(IClientSqlRepository clientSqlRepository, IPsychologistSqlRepository psychologistSqlRepository) : IRequestHandler<GetAvailableTimeSlotsQuery, List<AvailableTimeSlotsResult>>
{
    public async Task<List<AvailableTimeSlotsResult>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
    {
        var client = await clientSqlRepository.GetClientByIdAsync(request.ClientId, cancellationToken);
        var psychologistIds = client.GetPsychologists();

        var psychologists = await psychologistSqlRepository.GetPsychologistsByIdsAsync(psychologistIds, cancellationToken);

        var result = new List<AvailableTimeSlotsResult>();

        foreach (var psychologist in psychologists)
        {
            var availableTimeSlots = psychologist.GetAvailableTimeSlotsFrom(DateTime.UtcNow);

            if (availableTimeSlots.Count == 0)
                continue;

            var timeRanges = availableTimeSlots
                .Select(x => (TimeRange)x)
                .ToList();

            result.Add(new(psychologist.Id, timeRanges));
        }

        return result;
    }
}

public record AvailableTimeSlotsResult(long PsychologistId, List<TimeRange> AvailableTimeSlots);
