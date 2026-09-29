using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public int RentalContractId { get; set; }
        //عشان يقراء بيانات العقد حقت المبلغ 
        public RentalContract? RentalContract { get; set; }

     
        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } 

        public string Status { get; set; } 
    }
}
