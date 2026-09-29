using System.ComponentModel.DataAnnotations;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; } 
       
        public string NationalId { get; set; } 

        public string PhoneNumber { get; set; } 

        public string Email { get; set; } 
    }
}
