using System.ComponentModel.DataAnnotations;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "حقل اسم المستأجر مطلوب")]
        [Display(Name = "اسم المستأجر الكامل")]
        
        public string FullName { get; set; } 
        [Required(ErrorMessage = "حقل رقم الهوية مطلوب")]
        [Display(Name = "رقم الهوية / الإقامة")]
        [StringLength(10)]
        public string NationalId { get; set; } 

        [Required(ErrorMessage = "حقل رقم الجوال مطلوب")]
        [Display(Name = "رقم الجوال")]
        
        public string PhoneNumber { get; set; } 

        [EmailAddress(ErrorMessage = "الرجاء إدخال بريد إلكتروني صحيح")]
        [Display(Name = "البريد الإلكتروني")]
        
        public string Email { get; set; } 
    }
}
