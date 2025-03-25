using System.ComponentModel.DataAnnotations;

namespace Demo_SWD392_Coding.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string MedicineCode { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
