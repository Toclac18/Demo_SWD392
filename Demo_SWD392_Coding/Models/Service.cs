using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Service
{
    public string ServiceCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }
}
