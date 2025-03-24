using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo_SWD392_Coding.Models;

namespace Demo_SWD392_Coding.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly HospitalDbContext _context;

        public ReceptionistController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Receptionist
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.MedicalRecords.Include(m => m.PatientCodeNavigation);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Receptionist/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.PatientCodeNavigation)
                .FirstOrDefaultAsync(m => m.RecordCode == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // GET: Receptionist/Create
        public IActionResult Create()
        {
            ViewData["PatientCode"] = new SelectList(_context.Patients, "PatientCode", "PatientCode");
            return View();
        }

        // POST: Receptionist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordCode,PatientCode,CreatedAt")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientCode"] = new SelectList(_context.Patients, "PatientCode", "PatientCode", medicalRecord.PatientCode);
            return View(medicalRecord);
        }

        // GET: Receptionist/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            ViewData["PatientCode"] = new SelectList(_context.Patients, "PatientCode", "PatientCode", medicalRecord.PatientCode);
            return View(medicalRecord);
        }

        // POST: Receptionist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RecordCode,PatientCode,CreatedAt")] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.RecordCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalRecordExists(medicalRecord.RecordCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientCode"] = new SelectList(_context.Patients, "PatientCode", "PatientCode", medicalRecord.PatientCode);
            return View(medicalRecord);
        }

        // GET: Receptionist/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.PatientCodeNavigation)
                .FirstOrDefaultAsync(m => m.RecordCode == id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // POST: Receptionist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalRecordExists(string id)
        {
            return _context.MedicalRecords.Any(e => e.RecordCode == id);
        }
    }
}
