using Demo_SWD392_Coding.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Index()
        {
            var medicalRecords = _medicalRecordRepository.GetMedicalRecords();
            return View(medicalRecords);
        }

        // GET: MedicalRecord/Details/5
        public ActionResult Details(string id)
        {
            var medicalRecordDetails = _medicalRecordDetailRepository.GetMedicalRecords(id.ToString());
            return View(medicalRecordDetails);
        }

        
    }
}
