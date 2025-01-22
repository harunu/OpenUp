using System.Collections.Generic;

namespace iPractice.Api.Controllers.Psychologists.Dtos;

public record CreatePsychologistDto(string Name, List<long> InitialClients);
