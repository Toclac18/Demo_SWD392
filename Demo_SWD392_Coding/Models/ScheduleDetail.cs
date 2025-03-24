using System;
using System.Collections.Generic;

namespace Demo_SWD392_Coding.Models;

public partial class ScheduleDetail
{
    public string ScheduleCode { get; set; } = null!;

    public string? DoctorCode { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor? DoctorCodeNavigation { get; set; }
}
