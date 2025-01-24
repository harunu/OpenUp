using System.Collections.Generic;
using System.Linq;

namespace iPractice.Api.Models.Psychologists;

public class ClientAssignment
{
    public List<long> ClientIds { get; private set; } = new();
    public bool IsAssigned { get; private set; } = false; 

    private ClientAssignment()
    {
    }

    private ClientAssignment(List<long> clientIds)
    {
        ClientIds = clientIds;
        IsAssigned = clientIds.Any();
    }

    public static ClientAssignment InitializeFrom(List<long> clientIds) => new(clientIds);

    public void AssignNewClient(long clientId)
    {
        if (!ClientIds.Contains(clientId))
        {
            ClientIds.Add(clientId);
            IsAssigned = true; 
        }
    }

    public void DeAssignClient(long clientId)
    {
        if (ClientIds.Contains(clientId))
        {
            ClientIds.Remove(clientId);
            IsAssigned = ClientIds.Any(); 
        }
    }
}