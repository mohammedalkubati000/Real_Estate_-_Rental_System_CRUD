using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "رقم عقد الإيجار")]
        public int RentalContractId { get; set; }

        [ForeignKey("RentalContractId")]
        public RentalContract? RentalContract { get; set; }

        [Required(ErrorMessage = "حقل مبلغ الدفعة مطلوب")]
        [Display(Name = "المبلغ المدفوع")]
     
        public decimal AmountPaid { get; set; }

        [Required(ErrorMessage = "حقل تاريخ الدفع مطلوب")]
        [Display(Name = "تاريخ الدفع")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "حقل طريقة الدفع مطلوب")]
        [Display(Name = "طريقة الدفع")]
        
        public string PaymentMethod { get; set; } 

        [Required(ErrorMessage = "حقل حالة الدفعة مطلوب")]
        [Display(Name = "حالة الدفعة")]
        
        public string Status { get; set; } 
    }
}
