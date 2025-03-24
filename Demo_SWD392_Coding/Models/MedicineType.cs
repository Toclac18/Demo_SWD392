using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class MedicineType
{
    public string MedicineTypeCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
