using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Department
{
    public string DepartmentCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
