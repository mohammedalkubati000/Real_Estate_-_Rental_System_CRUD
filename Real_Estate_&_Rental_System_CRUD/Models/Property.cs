using System.ComponentModel.DataAnnotations;

namespace Real_Estate___Rental_System_CRUD.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        public string PropertyType { get; set; } 

        public string City { get; set; } 

        public string Address { get; set; } 

        public decimal AnnualRent { get; set; }

        public bool IsAvailable { get; set; }

      
    }
}
