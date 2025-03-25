using Demo_SWD392_Coding.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_SWD392_Coding.Service
{
    public interface IPatientService
    {
        Task<Patient> viewPatientÌnormation(string id);
        Task<IActionResult> updatePatientInfo(string id, Patient patient);
    }
}
