using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Demo_SWD392_Coding.Models;

namespace Demo_SWD392_Coding.ViewModels
{
    public class OrderViewModel
    {
        public List<Medicine> Medicines { get; set; }

        [Required]
        public string SelectedMedicineCode { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
