using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public int RentalContractId { get; set; }
        //عشان يقراء بيانات العقد حقت المبلغ 

        public RentalContract? RentalContract { get; set; }

     
        public decimal AmountPaid { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } 

        public string Status { get; set; } 
    }
}
