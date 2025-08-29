using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Domain
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
