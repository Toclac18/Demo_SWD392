using Demo_SWD392_Coding.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Demo_SWD392_Coding.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMedicalRecordDetailRepository _medicalRecordDetailRepository;

        public MedicalRecordController(IMedicalRecordRepository medicalRecordRepository, IMedicalRecordDetailRepository medicalRecordDetailRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _medicalRecordDetailRepository = medicalRecordDetailRepository;
        }

        // GET: MedicalRecord
        public ActionResult Index(string searchString)
        {
            var medicalRecords = _medicalRecordRepository.GetMedicalRecords();

            if (!string.IsNullOrEmpty(searchString))
            {
                medicalRecords = medicalRecords
                    .Where(m => m.PatientCodeNavigation.PatientCode.Contains(searchString) ||
                                m.PatientCodeNavigation.Fullname.Contains(searchString))
                    .ToList();
            }

            ViewData["CurrentFilter"] = searchString; // Giữ lại giá trị ô tìm kiếm
            return View(medicalRecords);
        }

        // GET: MedicalRecord/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var medicalRecordDetails = _medicalRecordDetailRepository.GetMedicalRecords(id);
            if (medicalRecordDetails == null)
            {
                return NotFound();
            }

            return View(medicalRecordDetails);
        }
    }
}
