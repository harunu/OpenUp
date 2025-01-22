using System.Collections.Generic;

namespace iPractice.Api.Controllers.Clients.Dtos;

public record CreateClientDto(string Name, List<long> InitialPsychologistIds);
