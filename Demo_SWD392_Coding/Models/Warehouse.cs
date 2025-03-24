using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Warehouse
{
    public string WarehouseCode { get; set; } = null!;

    public string? DepartmentCode { get; set; }

    public virtual Department? DepartmentCodeNavigation { get; set; }
}
