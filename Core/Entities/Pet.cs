using System;

namespace Core.Entities;

public class Pet : BaseEntity
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required char Gender { get; set; }
    public int Age { get; set; }
    public string? Breed { get; set; }
    public string? Characteristics { get; set; }
    public bool ForAdoption { get; set; }
    public DateTime DateAdopted { get; set; }
}
