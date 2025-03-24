namespace Demo_SWD392_Coding.ViewModels
{
    public class MedicalRecordDetailModel
    {
        public string RecordDetailCode { get; set; } = null!;
        public string? AppointmentCode { get; set; }
        public string RecordCode { get; set; } = null!;
        public string? DoctorCode { get; set; }
        public string? Result { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
