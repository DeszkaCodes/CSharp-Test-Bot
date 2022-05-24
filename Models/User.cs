#pragma warning disable CS8618
// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TestBot.Models;

public class User
{
    [StringLength(18)]
    public ulong Id { get; set; }

    [DefaultValue(0)]
    public uint Experience { get; set; } = 0;
}

#pragma warning restore CS8618