using System.Collections.Generic;

namespace iPractice.Api.Models.Clients;

public class PsychologistAssignment
{
    public List<long> PsychologistIds { get; set; } = [];

    private PsychologistAssignment()
    {
    }

    private PsychologistAssignment(List<long> psychologistIds)
    {
        PsychologistIds = psychologistIds;
    }

    public static PsychologistAssignment InitializeFrom(List<long> psychologistIds)
    {
        return new(psychologistIds);
    }

    public void AssignNewPsychologist(long psychologistId)
    {
        PsychologistIds.Add(psychologistId);
    }

    public void DeAssignPsychologist(long psychologistId)
    {
        PsychologistIds.Remove(psychologistId);
    }

    public bool IsAssigned(long psychologistId) => PsychologistIds.Contains(psychologistId);
}
