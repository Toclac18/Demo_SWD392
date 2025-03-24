using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class Patient
{
    public string PatientCode { get; set; } = null!;

    public int UserId { get; set; }

    public string? Fullname { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual User User { get; set; } = null!;
}
