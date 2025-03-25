using Demo_SWD392_Coding.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_SWD392_Coding.Service
{
    public class PatientService : IPatientService
    {
        private readonly HospitalDbContext _context;
        public PatientService(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> updatePatientInfo(string id, Patient patient)
        {
            if (id != patient.PatientCode)
            {
                Console.WriteLine($"❌ PatientCode {id} không khớp với model.");
                return new NotFoundResult();
            }

            try
            {
                var existingPatient = await _context.Patients
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.PatientCode == id);

                if (existingPatient == null)
                {
                    Console.WriteLine($"❌ Patient với PatientCode {id} không tồn tại!");
                    return new NotFoundResult();
                }

                Console.WriteLine($"✅ Found Patient: {existingPatient.PatientCode}, Fullname: {existingPatient.Fullname}, UserId: {existingPatient.UserId}");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == patient.UserId);
                if (user == null)
                {
                    Console.WriteLine($"❌ UserId {patient.UserId} không tồn tại trong bảng Users!");
                    return new BadRequestObjectResult("User không tồn tại.");
                }

                var existingPatientWithUser = await _context.Patients
                    .FirstOrDefaultAsync(p => p.UserId == patient.UserId && p.PatientCode != patient.PatientCode);
                if (existingPatientWithUser != null)
                {
                    Console.WriteLine($"❌ UserId {patient.UserId} đã được sử dụng bởi bệnh nhân khác: {existingPatientWithUser.PatientCode}");
                    return new BadRequestObjectResult($"User ID {patient.UserId} đã được sử dụng bởi bệnh nhân khác.");
                }

                Console.WriteLine($"🔄 Trước update: Fullname={existingPatient.Fullname}, Gender={existingPatient.Gender}, Phone={existingPatient.Phone}");

                existingPatient.Fullname = patient.Fullname;
                existingPatient.Gender = patient.Gender;
                existingPatient.Phone = patient.Phone;
                existingPatient.Address = patient.Address;
                existingPatient.Dob = patient.Dob;

                Console.WriteLine($"✅ Sau update: Fullname={existingPatient.Fullname}, Gender={existingPatient.Gender}, Phone={existingPatient.Phone}");

                _context.Update(existingPatient);
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ SaveChangesAsync() chạy thành công!");

                return new RedirectToActionResult("Details", "Patients", new { id = patient.PatientCode });
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("FOREIGN KEY") == true)
            {
                Console.WriteLine($"❌ Lỗi khóa ngoại: {ex.InnerException.Message}");
                return new BadRequestObjectResult("User ID không hợp lệ.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.PatientCode))
                {
                    Console.WriteLine($"❌ Concurrency Error: Patient {patient.PatientCode} không tồn tại.");
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi cập nhật bệnh nhân: {ex.Message}");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.PatientCode == id);
        }

        public async Task<Patient> viewPatientÌnormation(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var patient = await _context.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PatientCode == id);

            return patient;
        }
    }
}
