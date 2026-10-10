using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)] //    الي يكون في اعلى الصفحة(UUID) عشان ما يكرر المعرف 
    public class RentalContract // RentalContract = عقد الايجار
    {
        [Key]
        public int Id { get; set; } // حق رقم العقد
        public string Uuid { get; set; } = Guid.NewGuid().ToString(); //Random text
        public int PropertyId { get; set; }  // رقم العقار 

        public Property? Property { get; set; } // البيانات كاملة للعقار من رقم العقار

        public int TenantId { get; set; }  //رقم المستأجر

        public Tenant? Tenant { get; set; }

        public DateTime StartDate { get; set; }   

        public DateTime EndDate { get; set; }

        public decimal ContractValue { get; set; }  // قيمة العقد

        [NotMapped] // لازم احلها
        public string DisplayName => "عقد #" + Id + " - " + Tenant?.FullName;

    }
}
