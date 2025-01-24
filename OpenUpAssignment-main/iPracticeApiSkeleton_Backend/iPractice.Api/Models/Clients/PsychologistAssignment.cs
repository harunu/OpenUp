using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models.Clients;

public class PsychologistAssignment
{
    public List<long> PsychologistIds { get; private set; } = new();

    // Required marker property for EF Core
    public bool IsAssigned { get; private set; } = false;

    private PsychologistAssignment()
    {
    }

    private PsychologistAssignment(List<long> psychologistIds)
    {
        PsychologistIds = psychologistIds ?? new List<long>();
        IsAssigned = psychologistIds.Any(); 
    }

    public static PsychologistAssignment InitializeFrom(List<long> psychologistIds) => new(psychologistIds);

    public void AssignNewPsychologist(long psychologistId)
    {
        if (!PsychologistIds.Contains(psychologistId))
        {
            PsychologistIds.Add(psychologistId);
            IsAssigned = true; 
        }
    }

    public void DeAssignPsychologist(long psychologistId)
    {
        if (PsychologistIds.Contains(psychologistId))
        {
            PsychologistIds.Remove(psychologistId);
            IsAssigned = PsychologistIds.Any(); 
        }
    }
}