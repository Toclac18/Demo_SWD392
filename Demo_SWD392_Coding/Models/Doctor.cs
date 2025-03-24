using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Doctor
{
    public string DoctorCode { get; set; } = null!;

    public int UserId { get; set; }

    public string? DepartmentCode { get; set; }

    public string? SymptomSupport { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department? DepartmentCodeNavigation { get; set; }

    public virtual ICollection<MedicalRecordDetail> MedicalRecordDetails { get; set; } = new List<MedicalRecordDetail>();

    public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; } = new List<ScheduleDetail>();

    public virtual User User { get; set; } = null!;
}
