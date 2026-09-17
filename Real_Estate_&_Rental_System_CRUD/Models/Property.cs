using System.ComponentModel.DataAnnotations;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "حقل نوع العقار مطلوب")]
        [Display(Name = "نوع العقار")]
        
        public string PropertyType { get; set; } 

        [Required(ErrorMessage = "حقل المدينة مطلوب")]
        [Display(Name = "المدينة")]
        
        public string City { get; set; } 

        [Required(ErrorMessage = "حقل العنوان مطلوب")]
        [Display(Name = "العنوان بالتفصيل")]
      
        public string Address { get; set; } 

        [Required(ErrorMessage = "حقل قيمة الإيجار السنوي مطلوب")]
        [Display(Name = "الإيجار السنوي المطلوب (ريال)")]
       
        public decimal AnnualRent { get; set; }

        [Required]
        [Display(Name = "حالة العقار")]
        public bool IsAvailable { get; set; } 
    }
}
