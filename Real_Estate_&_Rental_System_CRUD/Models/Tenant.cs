using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

       
        public string FullName { get; set; } 
       
        public string NationalId { get; set; } 

        public string PhoneNumber { get; set; } 

        public string Email { get; set; } 
    }
}
