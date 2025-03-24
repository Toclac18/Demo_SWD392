using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class MedicalRecord
{
    public string RecordCode { get; set; } = null!;

    public string PatientCode { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<MedicalRecordDetail> MedicalRecordDetails { get; set; } = new List<MedicalRecordDetail>();

    public virtual Patient PatientCodeNavigation { get; set; } = null!;
}
