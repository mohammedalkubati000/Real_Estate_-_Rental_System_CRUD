using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Dtos
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public int PropertyTypeId { get; set; }
        public string? PropertyTypeName { get; set; }

        public string City { get; set; }

        public string Address { get; set; } 
        public decimal AnnualRent { get; set; } //الإيجار السنوي
        public bool IsAvailable { get; set; } = true;
    }
}
