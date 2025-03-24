using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo_SWD392_Coding.Models;

public partial class Patient
{
    [Required(ErrorMessage = "Patient Code is required.")]
    [StringLength(10, MinimumLength = 4, ErrorMessage = "Patient Code must be between 4 and 10 characters.")]
    public string PatientCode { get; set; } = null!;

    [Required(ErrorMessage = "User ID is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
    public string? Fullname { get; set; }

    [StringLength(10, ErrorMessage = "Gender must not exceed 10 characters.")]
    public string? Gender { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    [StringLength(15, MinimumLength = 9, ErrorMessage = "Phone number must be between 9 and 15 digits.")]
    public string? Phone { get; set; }

    [StringLength(200, ErrorMessage = "Address must not exceed 200 characters.")]
    public string? Address { get; set; }
    public DateOnly? Dob { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual User? User { get; set; }
}