using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo_SWD392_Coding.Models;
using Demo_SWD392_Coding.Service;

namespace Demo_SWD392_Coding.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var patient = await _patientService.viewPatientÌnormation(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var patient = await _patientService.viewPatientÌnormation(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PatientCode,UserId,Fullname,Gender,Phone,Address,Dob")] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            var result = await _patientService.updatePatientInfo(id, patient);
            return result;
        }
    }
}