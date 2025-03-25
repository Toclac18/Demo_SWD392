using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_SWD392_Coding.Models;

public partial class MedicalRecordDetail
{
    public string RecordDetailCode { get; set; } = null!;

    public string? AppointmentCode { get; set; }

    public string RecordCode { get; set; } = null!;

    public string? DoctorCode { get; set; }

    public string? Result { get; set; }
    [Column("createAt")]
    public DateTime CreatedAt { get; set; }

    public virtual Appointment? AppointmentCodeNavigation { get; set; }

    public virtual Doctor? DoctorCodeNavigation { get; set; }

    public virtual MedicalRecord RecordCodeNavigation { get; set; } = null!;
}
