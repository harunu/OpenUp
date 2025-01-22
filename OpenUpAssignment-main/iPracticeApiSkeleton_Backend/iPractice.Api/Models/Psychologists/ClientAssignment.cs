
using System.Collections.Generic;

namespace iPractice.Api.Models.Psychologists;

public class ClientAssignment
{
    public List<long> ClientIds { get; set; } = [];

    private ClientAssignment()
    {

    }

    private ClientAssignment(List<long> clientIds)
    {
        ClientIds = clientIds;
    }

    public static ClientAssignment InitializeFrom(List<long> clientIds) => new(clientIds);

    public void AssignNewClient(long clientId)
    {
        ClientIds.Add(clientId);
    }

    public void DeAssignClient(long clientId)
    {
        ClientIds.Remove(clientId);
    }

    public bool IsAssigned(long clientId) => ClientIds.Contains(clientId);
}