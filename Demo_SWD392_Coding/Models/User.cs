using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public virtual Admin? Admin { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Receptionist? Receptionist { get; set; }
}
