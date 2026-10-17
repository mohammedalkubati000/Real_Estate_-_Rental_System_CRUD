using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Application.Dtos
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
    public class PropertyCreateDto
    {
        public int PropertyTypeId { get; set; }
        public string City { get; set; } 
        public string Address { get; set; } 
        public decimal AnnualRent { get; set; }

    }

    public class PropertyUpdateDto : PropertyCreateDto
    {
        public int Id { get; set; }

        public string Uuid { get; set; }  //  عشان الروابط معتمده عليه
        public bool IsAvailable { get; set; }  // عشان العقار الجديد يكون مفتوح من نفسه 
    }
}
