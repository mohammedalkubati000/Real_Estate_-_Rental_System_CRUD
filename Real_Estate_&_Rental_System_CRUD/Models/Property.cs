using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Models
{
    [Index(nameof(Uuid), IsUnique = true)]
    public class Property //ملكية
    {
        [Key]
        public int Id { get; set; }

        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        public int PropertyTypeId { get; set; }
       // public string PropertyType { get; set; } //نوع الملكية
        public PropertyType? PropertyType { get; set; }
        public string City { get; set; } 

        public string Address { get; set; } 

        public decimal AnnualRent { get; set; } //الإيجار السنوي

        public bool IsAvailable { get; set; } = true;  //متاح او لا


    }
}
