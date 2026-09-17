using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class RentalContract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "العقار")]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required]
        [Display(Name = "المستأجر")]
        public int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant? Tenant { get; set; }

        [Required(ErrorMessage = "حقل تاريخ بدء العقد مطلوب")]
        [Display(Name = "تاريخ بدء العقد")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "حقل تاريخ انتهاء العقد مطلوب")]
        [Display(Name = "تاريخ انتهاء العقد")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "حقل قيمة الإيجار الفعلي مطلوب")]
        [Display(Name = "قيمة الإيجار المتفق عليه")]
        
        public decimal ContractValue { get; set; }

    }
}
