using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.Repository.IRepository;

namespace Demo_SWD392_Coding.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly HospitalDbContext _context;

        public MedicineRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Medicine> GetAllMedicines()
        {
            return _context.Medicines.ToList();
        }

        public IEnumerable<Medicine> SearchMedicine(string keyword)
        {
            return _context.Medicines
                .Where(m => m.Name.Contains(keyword))
                .ToList();
        }
    }

}
