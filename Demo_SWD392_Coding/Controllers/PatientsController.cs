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
    public class PatientsController : Controller
    {
        private readonly HospitalDbContext _context;

        public PatientsController(HospitalDbContext context)
        {
            _context = context;
        }
        // GET: Patients/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PatientCode == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.PatientCode == id);
            if (patient == null)
            {
                return NotFound();
            }

            // Không cần ViewData["UserId"] vì không hiển thị select
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PatientCode,UserId,Fullname,Gender,Phone,Address,Dob")] Patient patient)
        {
            if (id != patient.PatientCode)
            {
                Console.WriteLine($"❌ PatientCode {id} không khớp với model.");
                return NotFound();
            }

            // 🚀 Debug: In lỗi ModelState nếu có
            if (!ModelState.IsValid)
            {
                Console.WriteLine("===== ❌ DEBUG ModelState Errors =====");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}, Error: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                Console.WriteLine("======================================");
                return View(patient);
            }

            try
            {
                // 🚀 Debug: Kiểm tra xem Patient có tồn tại không
                var existingPatient = await _context.Patients
                    .Include(p => p.User) // Đảm bảo load dữ liệu User
                    .FirstOrDefaultAsync(p => p.PatientCode == id);

                if (existingPatient == null)
                {
                    Console.WriteLine($"❌ Patient với PatientCode {id} không tồn tại!");
                    return NotFound();
                }

                Console.WriteLine($"✅ Found Patient: {existingPatient.PatientCode}, Fullname: {existingPatient.Fullname}, UserId: {existingPatient.UserId}");

                // 🚀 Debug: Kiểm tra UserId có tồn tại trong bảng User không
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == patient.UserId);
                if (user == null)
                {
                    Console.WriteLine($"❌ UserId {patient.UserId} không tồn tại trong bảng Users!");
                    ModelState.AddModelError("UserId", "User không tồn tại.");
                    return View(patient);
                }

                // 🚀 Debug: Kiểm tra xem UserId đã được sử dụng bởi Patient khác chưa
                var existingPatientWithUser = await _context.Patients
                    .FirstOrDefaultAsync(p => p.UserId == patient.UserId && p.PatientCode != patient.PatientCode);
                if (existingPatientWithUser != null)
                {
                    Console.WriteLine($"❌ UserId {patient.UserId} đã được sử dụng bởi bệnh nhân khác: {existingPatientWithUser.PatientCode}");
                    ModelState.AddModelError("UserId", $"User ID {patient.UserId} đã được sử dụng bởi bệnh nhân khác.");
                    return View(patient);
                }

                // 🚀 Debug: Kiểm tra dữ liệu trước khi cập nhật
                Console.WriteLine($"🔄 Trước update: Fullname={existingPatient.Fullname}, Gender={existingPatient.Gender}, Phone={existingPatient.Phone}");

                // Cập nhật dữ liệu
                existingPatient.Fullname = patient.Fullname;
                existingPatient.Gender = patient.Gender;
                existingPatient.Phone = patient.Phone;
                existingPatient.Address = patient.Address;
                existingPatient.Dob = patient.Dob;

                // 🚀 Debug: Kiểm tra dữ liệu sau khi cập nhật
                Console.WriteLine($"✅ Sau update: Fullname={existingPatient.Fullname}, Gender={existingPatient.Gender}, Phone={existingPatient.Phone}");

                // Lưu thay đổi vào database
                _context.Update(existingPatient);
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ SaveChangesAsync() chạy thành công!");

                // Chuyển hướng về trang Details
                return RedirectToAction(nameof(Details), new { id = patient.PatientCode });
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("FOREIGN KEY") == true)
            {
                Console.WriteLine($"❌ Lỗi khóa ngoại: {ex.InnerException.Message}");
                ModelState.AddModelError("UserId", "User ID không hợp lệ.");
                return View(patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.PatientCode))
                {
                    Console.WriteLine($"❌ Concurrency Error: Patient {patient.PatientCode} không tồn tại.");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi cập nhật bệnh nhân: {ex.Message}");
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật bệnh nhân.");
                return View(patient);
            }
        }


        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.PatientCode == id);
        }
    }
}