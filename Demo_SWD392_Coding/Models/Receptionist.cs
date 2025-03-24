using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Receptionist
{
    public string ReceptionistCode { get; set; } = null!;

    public int UserId { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual User User { get; set; } = null!;
}
