using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Medicine
{
    public string MedicineCode { get; set; } = null!;

    public string MedicineTypeCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? Stock { get; set; }

    public virtual MedicineType MedicineTypeCodeNavigation { get; set; } = null!;
}
