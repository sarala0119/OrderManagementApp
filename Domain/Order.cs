using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementApp.Domain
{
    public class Order
    {
        [Key] public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Draft;
        public DateTime CreatedAt { get; set; }
        [Precision(18, 2)]
        public decimal TotalAmount { get; set; }


        public List<OrderItem> Items { get; set; } = new();


        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }
    }
}
