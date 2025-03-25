using Demo_SWD392_Coding.Models;

namespace Demo_SWD392_Coding.Repository.IRepository
{
    public interface IMedicineRepository
    {
        IEnumerable<Medicine> GetAllMedicines();
        IEnumerable<Medicine> SearchMedicine(string keyword);
    }

}
