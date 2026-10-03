using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)]  
    public class Payment // الدفع
    {
        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();//Random text 
        public int RentalContractId { get; set; }
        //عشان يقراء بيانات العقد حقت المبلغ 

        public RentalContract? RentalContract { get; set; } //RentalContract == عقد الايجار


        public decimal AmountPaid { get; set; } // AmountPaid= المبلغ المدفوع

        public DateTime PaymentDate { get; set; } //تاريخ الدفع

        public string PaymentMethod { get; set; } // طريقة الدفع

        public string Status { get; set; } // حالة الدفع 
    }
}
