using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class RentalContract
    {
        [Key]
        public int Id { get; set; }
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public int PropertyId { get; set; }

        public Property? Property { get; set; }

        public int TenantId { get; set; }

        public Tenant? Tenant { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal ContractValue { get; set; }

    }
}
