using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Appointment
{
    public string AppointmentCode { get; set; } = null!;

    public string? DoctorCode { get; set; }

    public string? PatientCode { get; set; }

    public string? ScheduleCode { get; set; }

    public string Status { get; set; } = null!;

    public virtual Doctor? DoctorCodeNavigation { get; set; }

    public virtual ICollection<MedicalRecordDetail> MedicalRecordDetails { get; set; } = new List<MedicalRecordDetail>();

    public virtual Patient? PatientCodeNavigation { get; set; }

    public virtual ScheduleDetail? ScheduleCodeNavigation { get; set; }
}
