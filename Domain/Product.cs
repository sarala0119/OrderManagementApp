using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Domain
{
    public class Product
    {
        [Key] 
        public Guid Id { get; set; }
        [Required] 
        public string Sku { get; set; } = null!;
        [Required] 
        public string Name { get; set; } = null!;
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
