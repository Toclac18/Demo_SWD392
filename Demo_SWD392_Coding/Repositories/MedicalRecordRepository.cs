using Demo_SWD392_Coding.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_SWD392_Coding.Repositories
{
    public interface IMedicalRecordRepository
    {
        List<MedicalRecord> GetMedicalRecords();
    }
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HospitalDbContext _context;
        public MedicalRecordRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public List<MedicalRecord> GetMedicalRecords()
        {
            return _context.MedicalRecords.Include(m => m.PatientCodeNavigation).ToList();
        }
    }
}
