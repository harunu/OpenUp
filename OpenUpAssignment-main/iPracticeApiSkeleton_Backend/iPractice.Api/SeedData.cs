using iPractice.Api.Models.Clients;
using iPractice.Api.Models.Psychologists;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api;

public class SeedData(ApplicationDbContext context)
{
    private const int NumberOfPsychologists = 20;
    private const int NumberOfClients = 50;
    private readonly List<InitialApplicationData> initialApplicationData = Generate();

    public void Seed()
    {
        context.Database.ExecuteSqlRaw("DELETE FROM AvailableTimeSlotsOfPsychologists");
        context.Database.ExecuteSqlRaw("DELETE FROM BookedAppointmentsOfPsychologists");
        context.Database.ExecuteSqlRaw("DELETE FROM Psychologists");

        context.Database.ExecuteSqlRaw("DELETE FROM ClientScheduledAppointments");
        context.Database.ExecuteSqlRaw("DELETE FROM Clients");

        var clients = CreateClients();
        context.Clients.AddRange(clients);
        context.Psychologists.AddRange(CreatePsychologists());
        context.SaveChanges();
    }

    private IEnumerable<Client> CreateClients()
    {
        return initialApplicationData
            .OrderBy(x => x.ClientId)
            .Select((data, i) =>
            {
                return Client.Create($"Client {i + 1}", [data.PsychologistId1, data.PsychologistId2]);
            });
    }

    private IEnumerable<Psychologist> CreatePsychologists()
    {
        return Enumerable
            .Range(1, NumberOfPsychologists)
            .Select(psychologistId =>
            {
                var clients = initialApplicationData
                    .Where(x => x.PsychologistId1 == psychologistId || x.PsychologistId2 == psychologistId)
                    .Select(x => x.ClientId)
                    .Distinct();

                return Psychologist.Create($"Psychologist {psychologistId}", clients.ToList());
            });
    }

    private static List<InitialApplicationData> Generate()
    {
        return Enumerable
            .Range(1, NumberOfClients)
            .Select(clientId =>
            {
                var random = new Random();

                var assignedPsychologistIds = Enumerable.Range(1, NumberOfPsychologists)
                    .OrderBy(_ => random.Next())
                    .Take(2)
                    .Select(x => (long)x)
                    .ToList();

                if (assignedPsychologistIds[0] == assignedPsychologistIds[1])
                {
                    assignedPsychologistIds[1] = Enumerable
                        .Range(1, NumberOfPsychologists)
                        .Where(num => num != assignedPsychologistIds[0])
                        .MinBy(_ => random.Next());
                }

                return new InitialApplicationData(
                    clientId,
                    assignedPsychologistIds.ElementAt(0),
                    assignedPsychologistIds.ElementAt(1));
            })
            .ToList();
    }

    public record InitialApplicationData(long ClientId, long PsychologistId1, long PsychologistId2);
}