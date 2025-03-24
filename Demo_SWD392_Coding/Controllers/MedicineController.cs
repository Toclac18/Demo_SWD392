using Demo_SWD392_Coding.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Demo_SWD392_Coding.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository ?? throw new ArgumentNullException(nameof(medicineRepository));
        }

        public IActionResult Index(string searchQuery)
        {
            var medicines = string.IsNullOrEmpty(searchQuery)
                ? _medicineRepository.GetAllMedicines()
                : _medicineRepository.SearchMedicine(searchQuery);

            if (!medicines.Any())
            {
                ViewBag.Message = "No medicine products in stock.";
            }

            return View(medicines);
        }
    }

}
