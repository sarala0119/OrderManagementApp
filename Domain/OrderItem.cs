using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementApp.Domain
{
    public class OrderItem
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPriceSnapshot { get; set; }
        [Precision(18, 2)]
        public decimal LineTotal { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
