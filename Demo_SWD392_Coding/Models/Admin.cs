using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Admin
{
    public string AdminCode { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
