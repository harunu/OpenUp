using System;

namespace iPractice.Api.Models;

public abstract class Entity
{
    public long Id { get; }
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}