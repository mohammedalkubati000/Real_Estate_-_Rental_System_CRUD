namespace Real_Estate___Rental_System_CRUD.Domain.Models
{
    public class PropertyType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation property for related properties
        public ICollection<Property>? Properties{get; set;}

    }
}
