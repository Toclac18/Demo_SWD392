using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo_SWD392_Coding.Repositories
{
    public interface IMedicalRecordDetailRepository
    {
        List<MedicalRecordDetailModel> GetMedicalRecords(string id);
    }
    public class MedicalRecordDetailRepository : IMedicalRecordDetailRepository
    {
        private readonly HospitalDbContext _context;
        public MedicalRecordDetailRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public List<MedicalRecordDetailModel> GetMedicalRecords(string id)
        {
            return _context.MedicalRecordDetails.Where(m => m.RecordCode.Equals(id)).Include(m => m.AppointmentCodeNavigation).Include(m => m.DoctorCodeNavigation.User).Include(m => m.RecordCodeNavigation.PatientCodeNavigation)
                .Select(m => new MedicalRecordDetailModel()
                {
                    RecordDetailCode = m.RecordDetailCode,
                    AppointmentCode = m.AppointmentCode,
                    RecordCode = m.RecordCode,
                    DoctorCode = m.DoctorCode,
                    CreatedAt = m.CreatedAt,
                    Result = m.Result,
                    PatientName = m.RecordCodeNavigation.PatientCodeNavigation.Fullname,
                    DoctorName = m.DoctorCodeNavigation.User.Fullname,
                }).ToList(); ;
        }
    }
}
